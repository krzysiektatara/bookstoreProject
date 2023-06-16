using BookStoreApplicationAPI.DAL;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Services.User;
using Microsoft.EntityFrameworkCore;
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

        public bool isEntityDeleted(IEntityBase entity)
        => _context.Entry(entity).State == EntityState.Deleted;

        public bool isEntityModified(IEntityBase entity)
        => _context.Entry(entity).State == EntityState.Modified;
    }
}
