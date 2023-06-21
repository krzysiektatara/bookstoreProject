
using BookStoreApplicationAPI.DAL;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.Data
{
    public static class SeedData
    {

        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<BookStoreDbContext>());
        }
        public static async Task AddTestData(BookStoreDbContext context)
        {

            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Bookings");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Book_store");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Users");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Products");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Roles");

            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('dbo.Roles', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('dbo.Products', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('dbo.Bookings', RESEED, 0)");

            //context.AddRange() => more efficient way to seed data
            //Read json file, deserialize, add range

            await context.Database.ExecuteSqlRawAsync("INSERT INTO dbo.Roles (Name) VALUES " +
                "('customer'), ('admin'), ('manager');");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO dbo.Users (Name, Address, Email, Phone, RoleId, Login, Password) VALUES" +
                "('User 1', 'Address 1', 'user1@example.com', '555-555-1234', 1, 'user1', 'password1'),\r\n  ('User 2', 'Address 2', 'user2@example.com', '555-555-2345', 1, 'user2', 'password2'),\r\n  ('User 3', 'Address 3', 'user3@example.com', '555-555-3456', 1, 'user3', 'password3'),\r\n  ('User 4', 'Address 4', 'user4@example.com', '555-555-4567', 1, 'user4', 'password4'),\r\n  ('User 5', 'Address 5', 'user5@example.com', '555-555-5678', 1, 'user5', 'password5'),\r\n  ('User 6', 'Address 6', 'user6@example.com', '555-555-6789', 1, 'user6', 'password6'),\r\n  ('User 7', 'Address 7', 'user7@example.com', '555-555-7890', 1, 'user7', 'password7'),\r\n  ('User 8', 'Address 8', 'user8@example.com', '555-555-8901', 1, 'user8', 'password8'),\r\n  ('User 9', 'Address 9', 'user9@example.com', '555-555-9012', 1, 'user9', 'password9'),\r\n  ('User 10', 'Address 10', 'user10@example.com', '555-555-0123', 1, 'user10', 'password10'),\r\n  ('AdminUser', 'Address', 'admin@example.com', '555-555-1123', 2, 'admin', 'password');");

            var usersIds = await context.Users.Select(x => x.Id).ToListAsync();
            await context.Database.ExecuteSqlRawAsync($"INSERT INTO dbo.Products (ProductName, description, author, price, image_path) VALUES " +
                $"('Product 1', 'Description 1', 'Author 1', 9.99, 'path/to/image1.jpg'),\r\n  ('Product 2', 'Description 2', 'Author 2', 14.99, 'path/to/image2.jpg'),\r\n  ('Product 3', 'Description 3', 'Author 3', 19.99, 'path/to/image3.jpg'),\r\n  ('Product 4', 'Description 4', 'Author 4', 12.99, 'path/to/image4.jpg'),\r\n  ('Product 5', 'Description 5', 'Author 5', 24.99, 'path/to/image5.jpg'),\r\n  ('Product 6', 'Description 6', 'Author 6', 11.99, 'path/to/image6.jpg'),\r\n  ('Product 7', 'Description 7', 'Author 7', 15.99, 'path/to/image7.jpg'),\r\n  ('Product 8', 'Description 8', 'Author 8', 17.99, 'path/to/image8.jpg'),\r\n  ('Product 9', 'Description 9', 'Author 9', 22.99, 'path/to/image9.jpg'),\r\n  ('Product 10', 'Description 10', 'Author 10', 29.99, 'path/to/image10.jpg');");

            var productsIds = await context.Products.Select(x => x.Id).ToListAsync();
            await context.Database.ExecuteSqlRawAsync($"INSERT INTO dbo.Bookings (UserId, ProductId, delivery_address, status_id, quantity) VALUES" +
                $"({usersIds[5]}, {productsIds[2]}, 'Address 1', 1, 2),\r\n  ({usersIds[1]}, {productsIds[1]},'Address 2', 1, 3),\r\n  ({usersIds[5]}, {productsIds[0]}, 'Address 3', 1, 4),\r\n  ({usersIds[2]}, {productsIds[3]}, 'Address 4', 1, 5),\r\n  ({usersIds[0]}, {productsIds[3]}, 'Address 5', 1, 6);");

            await context.Database.ExecuteSqlRawAsync($"INSERT INTO dbo.book_store (ProductId, available_qty, booked_qty, sold_qty) VALUES" +
                $"({productsIds[0]}, 10, 4, 2),\r\n  ({productsIds[1]}, 12, 3, 5),\r\n  ({productsIds[2]}, 3, 2, 0),\r\n  ({productsIds[3]}, 6, 11, 9),\r\n  ({productsIds[4]}, 4, 0, 0);");
            await context.SaveChangesAsync();
        }
    }
}
