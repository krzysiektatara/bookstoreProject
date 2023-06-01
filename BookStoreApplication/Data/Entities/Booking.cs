using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlTypes;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Product_Id { get; set; }
        public string Delivery_Address { get; set; }
        public int Status_Id { get; set; }
        public int Quantity { get; set; }
    }
}
