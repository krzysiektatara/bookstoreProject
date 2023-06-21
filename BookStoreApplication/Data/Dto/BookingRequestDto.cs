using Microsoft.Build.Framework;
using Microsoft.OpenApi.Attributes;

namespace BookStoreApplicationAPI.Data.Dto
{
    public class BookingRequestDto
    {
        [Required]
        [Display(name: "product ID")]
        public int ProductId { get; set; }

        [Required]
        [Display(name: "Requested quantity")]
        public int Quantity { get; set; }

        [Display(name: "Delivery address")]
        public string? Delivery_Address { get; set; }
    }
}
