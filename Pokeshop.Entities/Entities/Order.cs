using System;
using System.Collections.Generic;

namespace Pokeshop.Entities.Entities
{
    public class Order
    {
        public string OrderCode { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTime DateOrdered { get; set; }
        public DateTime Modified { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Invoice Invoice { get; set; }
        public ApplicationUser User { get; set; }
    }
}
