using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.CartDtos
{
    public class CartUpdateDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
