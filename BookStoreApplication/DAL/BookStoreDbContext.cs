
using Microsoft.EntityFrameworkCore;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.DAL
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<StoreItem> Book_store { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;
    }
}
