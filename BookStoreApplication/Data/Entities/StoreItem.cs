namespace BookStoreApplicationAPI.Data.Entities
{
    public class StoreItem : Entity
    {
        //public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Available_qty { get; set; }
        public int Booked_qty { get; set; }
        public int Sold_qty { get; set; }
    }
}
