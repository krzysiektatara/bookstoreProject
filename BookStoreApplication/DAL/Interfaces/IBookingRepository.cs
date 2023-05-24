using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<BookingEntity> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(CreateBookingDto booking);
        Task<bool> ReviewBooking(BookingRequestDto booking);
    }
}
