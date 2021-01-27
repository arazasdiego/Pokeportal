using Pokeshop.Api.Dtos.ProductDtos;
using System;

namespace Pokeshop.Api.Dtos.CartDtos
{
    public class CartReadDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductCartReadDto Product { get; set; }
    }
}
