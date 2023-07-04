using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Name { get; set; }
    }
}
