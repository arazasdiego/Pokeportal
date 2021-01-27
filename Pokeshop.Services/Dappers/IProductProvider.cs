using Pokeshop.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByPageAsync(int pageNumber, int pageSize);
        Task<Product> GetByIdAsync(int id);
        Task<int> CountAsync();
        Task<int> AddAsync(Product newProduct);
        Task<int> UpdateAsync(Product modifiedProduct);
        Task<int> DeleteAsync(int id);
        Task<int> AddStockAsync(int id, int quantityAdded);
        Task<decimal> GetPriceAsync(int id);
    }
}
