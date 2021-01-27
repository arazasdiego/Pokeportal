using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface IIdentityProvider
    {
        Task<ApplicationUser> LoginAsync(string email, string password);
        Task<int> RegisterAsync(ApplicationUser newUser);
        Task<IEnumerable<ApplicationUser>> GetByPageAsync(int pageNumber, int pageSize);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<int> CountAsync();
        Task<int> UpdateAsync(ApplicationUser modifiedUser);
        Task<int> DeleteAsync(string userId);
    }
}
