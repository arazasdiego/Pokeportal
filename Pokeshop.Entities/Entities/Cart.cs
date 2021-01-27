using System;

namespace Pokeshop.Entities.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
