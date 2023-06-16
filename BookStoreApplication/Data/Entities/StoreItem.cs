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
        public int Product_Id { get; set; }
        public int Available_qty { get; set; }
        public int Booked_qty { get; set; }
        public int Sold_qty { get; set; }
    }
}
