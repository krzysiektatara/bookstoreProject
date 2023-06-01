
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Models
{
    public class BookingWithProduct
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public Product Product { get; set; }
        public string Delivery_Address { get; set; }
        public int Status_Id { get; set; }
        public int Quantity { get; set; }
    }
}
