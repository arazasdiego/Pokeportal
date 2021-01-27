using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface IOrderProvider
    {
        Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize, OrderStatus status);
        Task<IEnumerable<Order>> GetAllByUserAsync(int pageNumber, int pageSize, string userId);
        Task<IEnumerable<Order>> GetAllByUserAsync(int pageNumber, int pageSize, string userId, OrderStatus status);

        Task<Order> GetByOrderCodeAsync(string orderCode);
        Task<Order> GetByUserAsync(string userId, string orderCode);

        Task<int> CountAllOrdersAsync();
        Task<int> CountAllOrdersAsync(OrderStatus status);
        Task<int> CountByUserAsync(string userId);
        Task<int> CountByUserAsync(string userId, OrderStatus status);

        Task<int> PlaceOrderAsync(Order newOrder);
        Task<int> DeleteAsync(string orderCode);
        
        Task<int> CountUserOrderAsync(string userId);
        Task<decimal> GetTotalPayableAsync(string userId, string orderCode);
        Task<int> UpdateStatusAsync(OrderStatus orderStatus, string orderCode);
        Task<bool> OrderExistsAsync(string orderCode);
    }
}
