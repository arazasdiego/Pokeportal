using Pokeshop.Api.Dtos.ProductDtos;

namespace Pokeshop.Api.Dtos.OrderItemDtos
{
    public class OrderItemReadDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductReadDto Product { get; set; }
    }
}
