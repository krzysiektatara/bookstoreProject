using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Data.Enums;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; } = default!;
        public DbSet<UserEntity> Users { get; set; } = default!;
        public DbSet<BookingEntity> Bookings { get; set; } = default!;
        public DbSet<StoreItemEntity> Book_store { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;
    }
}
