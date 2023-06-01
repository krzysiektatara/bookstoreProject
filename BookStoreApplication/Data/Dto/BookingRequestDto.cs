using Microsoft.Build.Framework;
using Microsoft.OpenApi.Attributes;

namespace BookStoreApplicationAPI.Data.Dto
{
    public class BookingRequestDto
    {
        [Required]
        [Display(name: "productID")]
        public int Product_Id { get; set; }
        [Required]
        [Display(name: "quantity")]
        public int Requested_qty { get; set; }
    }
}
