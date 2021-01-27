using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.CategoryDtos
{
    public class CategoryAddDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
