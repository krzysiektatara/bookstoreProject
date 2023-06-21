using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class StoreItem : IEntityBase
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Available_qty { get; set; }
        [Required]
        public int Booked_qty { get; set; }
        [Required]
        public int Sold_qty { get; set; }
    }
}
