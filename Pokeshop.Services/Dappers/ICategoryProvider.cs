using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface ICategoryProvider
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetByPageAsync(int pageNumber, int pageSize);
        Task<Category> GetByIdAsync(int id);
        Task<int> CountAsync();
        Task<int> AddAsync(Category newCategory);
        Task<int> UpdateAsync(Category modifiedCategory);
        Task<int> DeleteAsync(int id);
    }
}
