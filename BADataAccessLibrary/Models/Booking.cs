using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class Booking : IEntityBase
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        //public Product Product { get; set; }

        [MaxLength(500)]
        public string? Delivery_Address { get; set; }

        [DefaultValue(1)]
        public int Status_Id { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
