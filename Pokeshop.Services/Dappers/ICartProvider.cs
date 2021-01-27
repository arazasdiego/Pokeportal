using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface ICartProvider
    {
        Task<IEnumerable<Cart>> GetAsync(string userId);
        Task<int> AddAsync(Cart newItem);
        Task<int> UpdateAsync(Cart modifiedItem);
        Task DeleteAsync(IEnumerable<int> productIds, string userId);
        Task<bool> ExistsAsync(int productId, string userId);
        Task<decimal> GetTotalAmountAsync(string userId);
        Task<bool> HasUserCartAsync(string userId);
    }
}
