using System;

namespace Pokeshop.Entities.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
