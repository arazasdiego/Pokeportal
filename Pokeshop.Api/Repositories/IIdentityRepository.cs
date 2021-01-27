using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.IdentityDtos;
using Pokeshop.Api.Models.Identity;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public interface IIdentityRepository
    {
        Task<int> DeleteAsync(string userId);
        Task<IdentityReadDto> GetByIdAsync(string userId);
        Task<PagedList<IdentityReadDto>> GetByPageAsync(int pageNumber, int pageSize);
        Task<LoginResponse> LoginAsync(LoginVm model);
        Task<int> RegisterAsync(RegisterDto model);
        Task<int> UpdateAsync(IdentityUpdateDto model);
        TokenVm GenerateJwtToken(string userId, string email, string secret);
    }
}