using System.Collections.Generic;

namespace Pokeshop.Api.Models.Carts
{
    public class CartDeleteVm
    {
        public IEnumerable<int> ProductIds { get; set; }
    }
}
