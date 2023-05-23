using BookStoreApplicationAPI.Data;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Services.User;
using Microsoft.Extensions.Options;

namespace BookStoreApplicationAPI.Services.Booking
{
    public class DefaultBookingLogicService : IBookingLogicService
    {
        private readonly BookStoreDbContext _context;
        public DefaultBookingLogicService(BookStoreDbContext context)
        {
            _context = context;
        }

        public bool isRequestetProductAvailable(int requested_qty, int available_qty)
        => available_qty - requested_qty >=0;
    }
}
