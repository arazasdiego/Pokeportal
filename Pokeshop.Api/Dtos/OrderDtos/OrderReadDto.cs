using Pokeshop.Entities.Entities;
using System;

namespace Pokeshop.Api.Dtos.OrderDtos
{
    public class OrderReadDto
    {
        public string OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DateOrdered { get; set; }
    }
}
