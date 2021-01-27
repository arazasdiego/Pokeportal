using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.OrderDtos;
using Pokeshop.Entities.Entities;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<PagedList<OrderReadDto>> GetAllAsync(int pageNumber, int pageSize, OrderStatus orderStatus); 
        Task<PagedList<OrderReadDto>> GetAllByUserAsync(int pageNumber, int pageSize, string userId, OrderStatus orderStatus);

        Task<OrderFullReadDto> GetByOrderCodeAsync(string orderCode);
        Task<OrderFullReadDto> GetByUserAsync(string userId, string orderCode);
        
        Task<bool> PlaceOrderAsync(string userId);
        Task<bool> PayAsync(string orderCode, decimal amountPaid);
        Task<int> DeleteAsync(string orderCode);
        Task<int> ChangeStatusAsync(string orderCode);
    }
}
