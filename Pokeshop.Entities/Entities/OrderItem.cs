﻿namespace Pokeshop.Entities.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
    }
}
