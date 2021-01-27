using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Parameters
{
    public class OrderParameter : QueryStringParameter
    {
        public OrderStatus OrderStatus { get; set; }
    }
}
