using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApplicationAPI.Data.Models
{
    public class ProductWithResource : Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Image_Path { get; set; }
    }
}
