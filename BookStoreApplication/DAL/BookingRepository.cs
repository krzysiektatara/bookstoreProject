using AutoMapper;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookingRepository(
            IMapper mapper,
            BookStoreDbContext context)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(CreateBookingDto booking)
        {
            var newBooking = _context.Bookings.Add(_mapper.Map<BookingEntity>(booking));

            //var created = await _context.SaveChangesAsync();
            //if (created < 1) throw new InvalidOperationException("Could not create booking.");
        }



        public async Task<BookingEntity> GetBookingByIdAsync(int id)
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
