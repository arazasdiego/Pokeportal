using System;

namespace Pokeshop.Api.Dtos.InvoiceDtos
{
    public class InvoiceReadDto
    {
        public string OrderCode { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
