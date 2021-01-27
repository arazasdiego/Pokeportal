using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.CartDtos
{
    public class CartAddDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
