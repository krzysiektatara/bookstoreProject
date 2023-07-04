
using BADataAccessLibrary.Models;
using BookStoreApplicationAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                    .Entity<Role>()
                    .HasData(Enum.GetValues(typeof(RoleEnum))
                        .Cast<RoleEnum>()
                        .Select(e => new Role
                        {
                            Id = (short)e,
                            Name = e.ToString()
                        })
            );
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<StoreItem> Book_store { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;
    }
}
