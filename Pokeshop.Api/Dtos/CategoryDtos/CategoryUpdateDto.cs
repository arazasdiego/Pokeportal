using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
