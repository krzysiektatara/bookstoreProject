using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public abstract class Entity
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BindNever]
        //[BindProperty(Name = "Id", SupportsGet = false)]
        public int Id { get; set; }
    }
}
