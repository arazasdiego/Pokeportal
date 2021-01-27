using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.CategoryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryReadDto>> GetAllAsync();
        Task<PagedList<CategoryReadDto>> GetPageAsync(int pageNumber, int pageSize);
        Task<CategoryReadDto> GetByIdAsync(int categoryId);
        Task<int> AddAsync(CategoryAddDto model);
        Task<int> UpdateAsync(CategoryUpdateDto model);
        Task<int> DeleteAsync(int categoryId);
    }
}
