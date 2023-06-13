using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Services.User;
using AutoMapper;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Data.Exceptions;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingLogicService _bookingLogicService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(
            IBookingLogicService bookingLogicService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookingLogicService = bookingLogicService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<BookingWithProduct>> GetBookingById(int id)
        {
            var bookingEntity = await _unitOfWork.Bookings.GetBookingByIdAsync(id);
            //var b = _unitOfWork.get().GetByID(id);
            if (bookingEntity == null) return NotFound();
            var product = await _unitOfWork.Products.GetAsyncById(bookingEntity.Product_Id);

            var booking = _mapper.Map<Booking, BookingWithProduct>(bookingEntity);
            booking.Product = product;

            return booking;
        }

        // POST: api/Products
        /// <summary>
        /// Create booking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookingForm"></param>
        /// <returns></returns>
        [HttpPost("{userId}/bookings")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Booking>> CreateBooking(
            int userId, [FromBody] BookingRequestDto bookingForm)
        {
            var user = await _unitOfWork.Users.GetUserWithAdressAsync(userId);
            var product = await _unitOfWork.Products.GetAsyncById(bookingForm.Product_Id);
            var storeItem = await _unitOfWork.Store.GetAsyncById(bookingForm.Product_Id);

            var isRequestedQty_Available = _bookingLogicService.isRequestetProductAvailable(bookingForm.Requested_qty, storeItem.Available_qty);
            if (user == null || product == null) return NotFound();
            var itemUpdate = new ItemQuantityDto
            {
                Product_Id = bookingForm.Product_Id,
                Requested_qty = bookingForm.Requested_qty
            };
            if (isRequestedQty_Available)
            {
                var updatedProductQty = await _unitOfWork.Store.UpdateProductQuantity(itemUpdate);
                if (updatedProductQty == null) return BadRequest
                    (
                    new ApiError($"Unable to update.")
                    );
            }
            else
                return BadRequest
                    (
                    new ApiError($"available quality is {storeItem.Available_qty}, requested is {bookingForm.Requested_qty}.")
                    );
                

            var booking = _mapper.Map<UserAdressDto, CreateBookingDto>(user);
            booking.Product_Id = bookingForm.Product_Id;
            booking.Quantity = bookingForm.Requested_qty;
            await _unitOfWork.Bookings.CreateBookingAsync(booking);
            _unitOfWork.SaveAsync();
            return Created(string.Empty, booking);
        }
    }
}

