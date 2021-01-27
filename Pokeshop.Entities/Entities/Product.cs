using System;

namespace Pokeshop.Entities.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Category Category { get; set; }
    }
}
