using AutoMapper;
using BookStoreApplicationAPI.DAL;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using BookStoreApplicationAPI.Services.User;

namespace BookStoreApplicationAPI.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BookStoreDbContext _context;
        public IBookingRepository Bookings { get; }
        public IProductRepository<ProductWithResource> Products { get; }
        public IStoreRepository Store { get; }
        public IUserRepository Users { get; }

  

        public UnitOfWork(
            BookStoreDbContext context,
            IBookingRepository bookingRepository,
            IProductRepository<ProductWithResource> productRepository,
            IStoreRepository storeRepository,
            IUserRepository userRepository
            )
        {
            _context = context;
            Bookings = bookingRepository;
            Products = productRepository;
            Store = storeRepository;
            Users = userRepository;
        }

        private bool disposed = false;

        public void SaveAsync()
            => _context.SaveChangesAsync();

        public void Save()
            => _context.SaveChanges();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
