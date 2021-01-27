using AutoMapper;
using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.ProductDtos;
using Pokeshop.Api.Models.Products;
using Pokeshop.Entities.Entities;
using Pokeshop.Services.Dappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductProvider _productProvider;
        private readonly IMapper _mapper;

        public ProductRepository(IProductProvider productProvider, IMapper mapper)
        {
            _productProvider = productProvider;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductAddDto model)
        {
            var newProduct = _mapper.Map<Product>(model);
            return await _productProvider.AddAsync(newProduct);
        }

        public async Task<int> AddStockAsync(ProductAddStockVm model)
        {
            return await _productProvider.AddStockAsync(model.ProductId, model.Quantity);
        }

        public async Task<int> DeleteAsync(int productId)
        {
            return await _productProvider.DeleteAsync(productId);
        }

        public async Task<ProductReadDto> GetByIdAsync(int productId)
        {
            var product = await _productProvider.GetByIdAsync(productId);
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<PagedList<ProductReadDto>> GetByPageAsync(int pageNumber, int pageSize)
        {
            var products = await _productProvider.GetByPageAsync(pageNumber, pageSize);
            var productsCount = await _productProvider.CountAsync();
            var items = _mapper.Map<IEnumerable<ProductReadDto>>(products);

            return PagedList<ProductReadDto>.ToPagedList(pageNumber, pageSize, productsCount, items);
        }

        public async Task<int> UpdateAsync(ProductUpdateDto model)
        {
            var modifiedProduct = _mapper.Map<Product>(model);
            return await _productProvider.UpdateAsync(modifiedProduct);
        }
    }
}
