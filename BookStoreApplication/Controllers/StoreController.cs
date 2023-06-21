using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.DAL.Services;
using BookStoreApplicationAPI.Data.Dto;


namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController (IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<StoreItem>> GetStoreItem(int id)
        {
            var product = await _storeService.GetStoreItemByIdAsync(id);
            if (product == null) return NotFound();

            return product;
        }


        /// <summary>
        /// update available quantity of store item.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPatch("{productId}/{quantity}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StoreItem>> UpdateStoreItem(int productId, int quantity)
        {
            await _storeService.UpdateStoreItemAsync(productId, quantity);
            return Ok();
        }

        /// <summary>
        /// Create booking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookingForm"></param>
        /// <returns></returns>
        [HttpPost("{userId}/booking")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Booking>> CreateBooking(
    int userId, [FromBody] BookingRequestDto bookingForm)
        {
            var booking = await _storeService.CreateBookingAsync(bookingForm, userId);

            return Created(string.Empty, booking);
        }

        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        [HttpGet("booking/{bookingId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Booking>> GetBookingById(int bookingId)
        {
            var booking = await _storeService.GetBookingByIdAsync(bookingId);        
            return booking;
        }

    }
    }