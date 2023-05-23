using BookStoreApplicationAPI.Repositories.Interfaces;

namespace BookStoreApplicationAPI.Repositories.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IProductRepository Products { get; }
        IStoreRepository Store { get; }
        IUserRepository Users { get; }
        public void Save();
    }
}
