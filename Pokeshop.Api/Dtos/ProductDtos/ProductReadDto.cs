using Pokeshop.Api.Dtos.CategoryDtos;
using System;

namespace Pokeshop.Api.Dtos.ProductDtos
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }  
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public CategoryReadDto Category { get; set; }
    }
}
