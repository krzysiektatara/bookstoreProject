namespace BookStoreApplicationAPI.Models
{
    public class Product : Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Image_Path { get; set; }
    }
}
