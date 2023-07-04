using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApplicationAPI.Data.Models
{
    public class ProductWithResource : Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
