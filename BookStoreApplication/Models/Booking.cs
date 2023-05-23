using System.Data;
using System.Data.SqlTypes;

namespace BookStoreApplicationAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public ProductEntity Product { get; set; }
        public string Delivery_Address { get; set; }
        public int Status_Id { get; set; }
        public int Quantity { get; set; }
    }
}
