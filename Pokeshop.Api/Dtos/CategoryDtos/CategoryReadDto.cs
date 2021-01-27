using System;

namespace Pokeshop.Api.Dtos.CategoryDtos
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
