using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.ProductDtos;
using Pokeshop.Api.Models.Products;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public interface IProductRepository
    {
        Task<PagedList<ProductReadDto>> GetByPageAsync(int pageNumber, int pageSize);
        Task<ProductReadDto> GetByIdAsync(int productId);
        Task<int> AddAsync(ProductAddDto model);
        Task<int> AddStockAsync(ProductAddStockVm model);
        Task<int> UpdateAsync(ProductUpdateDto model);
        Task<int> DeleteAsync(int productId);
    }
}
