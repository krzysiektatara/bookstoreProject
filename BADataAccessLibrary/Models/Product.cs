using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class Product : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]     
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(10)")]
        public string Image_Path { get; set; }
    }
}
