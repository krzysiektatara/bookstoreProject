using BADataAccessLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class User : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //This works when LazyLoaded proxies is added in context.
        //public virtual Role Role { get; set; }

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

        //public RoleEnum RoleEnum => (RoleEnum)RoleId;
        [JsonIgnore]
        [NotMapped]
        public RoleEnum RoleEnum
        {
            get => (RoleEnum)RoleId;
            set => RoleId = (short)value;
        }
    }
}
