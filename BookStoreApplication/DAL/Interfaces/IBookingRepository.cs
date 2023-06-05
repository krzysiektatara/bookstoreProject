using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(CreateBookingDto booking);
        Task<bool> ReviewBooking(BookingRequestDto booking);
    }
}
