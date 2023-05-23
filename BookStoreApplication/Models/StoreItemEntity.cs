namespace BookStoreApplicationAPI.Models
{
    public class StoreItemEntity
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Available_qty { get; set; }
        public int Booked_qty { get; set; }
        public int Sold_qty { get; set; }
    }
}
