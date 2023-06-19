using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class User : IEntityBase
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar(10)")]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Password { get; set; }

    }
}
