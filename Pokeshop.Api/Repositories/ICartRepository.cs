using Pokeshop.Api.Dtos.CartDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddAsync(CartAddDto model, string userId);
        Task DeleteAsync(IEnumerable<int> productids, string userId);
        Task<IEnumerable<CartReadDto>> GetAsync(string userId);
        Task<int> UpdateAsync(CartUpdateDto model, string userId);
    }
}