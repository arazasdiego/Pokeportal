using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Models.Orders
{
    public class OrderPayVm
    {
        [Required]
        public string OrderCode { get; set; }
        [Required]
        public decimal AmountPaid { get; set; }
    }
}
