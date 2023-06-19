namespace BookStoreApplicationAPI.Data.Dto
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Image_Path { get; set; }
    }
}
