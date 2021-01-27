using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface IOrderItemProvider
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(string orderCode);
        Task AddOrderItemAsync(IEnumerable<OrderItem> orderItems);
        Task<int> DeleteAsync(string orderCode);
    }
}
