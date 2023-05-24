using AutoMapper;
using BookStoreApplicationAPI.DAL;
using BookStoreApplicationAPI.Repositories.Interfaces;
using BookStoreApplicationAPI.Services.User;

namespace BookStoreApplicationAPI.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BookStoreDbContext _context;
        public IBookingRepository Bookings { get; }
        public IProductRepository Products { get; }
        public IStoreRepository Store { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(
            BookStoreDbContext context,
            IBookingRepository bookingRepository,
            IProductRepository productRepository,
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

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;



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
