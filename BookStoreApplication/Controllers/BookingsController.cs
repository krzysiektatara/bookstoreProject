﻿using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Services.User;
using BookStoreApplicationAPI.Exceptions;
using AutoMapper;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.UOW;
using BookStoreApplicationAPI.Repositories;

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
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var bookingEntity = await _unitOfWork.Bookings.GetBookingByIdAsync(id);
            if (bookingEntity == null) return NotFound();
            var product = await _unitOfWork.Products.Geta(bookingEntity.Product_Id);

            var booking = _mapper.Map<BookingEntity, Booking>(bookingEntity);
            booking.Product = product.Value;

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
        public async Task<ActionResult<BookingEntity>> CreateBooking(
            int userId, [FromBody] BookingRequestDto bookingForm)
        {
            var user = await _unitOfWork.Users.GetUserWithAdressAsync(userId);
            var product = await _unitOfWork.Products.Geta(bookingForm.Product_Id);
            var storeItem = await _unitOfWork.Store.GetByProductIdAsync(bookingForm.Product_Id);

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
            _unitOfWork.Save();
            return Created(string.Empty, booking);
        }
    }
}
