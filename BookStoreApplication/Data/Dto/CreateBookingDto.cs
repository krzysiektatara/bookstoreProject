namespace BookStoreApplicationAPI.Data.Dto
{
    public class CreateBookingDto
    {
        public int User_Id { get; set; }
        public int? Product_Id { get; set; }
        public string? Delivery_Address { get; set; }
        public int Quantity { get; set; }
    }
}
