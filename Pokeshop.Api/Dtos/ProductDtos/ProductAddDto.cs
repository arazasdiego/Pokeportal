using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.ProductDtos
{
    public class ProductAddDto
    {  
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(60)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
