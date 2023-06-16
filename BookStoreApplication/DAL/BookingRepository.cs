using AutoMapper;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Repositories.Interfaces;
using BookStoreApplicationAPI.Services.User;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookingLogicService _bookingLogicService;
        public BookingRepository(
             IBookingLogicService bookingLogicService,
            IMapper mapper,
            BookStoreDbContext context)
        {
            _bookingLogicService = bookingLogicService;
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(CreateBookingDto booking)
        {
            var newBooking = _context.Bookings.Add(_mapper.Map<Booking>(booking));
        }



        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.SingleOrDefaultAsync(x => x.Id == id);
            return booking;
        }

        public Task<bool> ReviewBooking(BookingRequestDto booking)
        {
            throw new NotImplementedException();
        }
    }
}
