using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApplicationAPI.Data.Dto
{
    public class AddStoreItemDto
    {
        public int ProductId { get; set; }
        public int? Available_qty { get; set; }

    }
}
