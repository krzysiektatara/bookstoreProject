using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreApplicationAPI.Data.Entities
{
    public interface IEntityBase
    {
        //[JsonIgnore]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BindNever]
        //[BindProperty(Name = "Id", SupportsGet = false)]
        int Id { get; set; }
    }
}
