using Pokeshop.Api.Dtos.InvoiceDtos;
using Pokeshop.Api.Dtos.OrderItemDtos;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;

namespace Pokeshop.Api.Dtos.OrderDtos
{
    public class OrderFullReadDto
    {
        public string OrderCode { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime Modified { get; set; }
        public IEnumerable<OrderItemReadDto> OrderItems { get; set; }
        public InvoiceReadDto Invoice { get; set; }
    }
}
