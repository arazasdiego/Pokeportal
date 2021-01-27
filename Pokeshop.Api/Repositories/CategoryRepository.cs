using AutoMapper;
using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.CategoryDtos;
using Pokeshop.Entities.Entities;
using Pokeshop.Services.Dappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly IMapper _mapper;

        public CategoryRepository(ICategoryProvider categoryProvider, IMapper mapper)
        {
            _categoryProvider = categoryProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _categoryProvider.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<PagedList<CategoryReadDto>> GetPageAsync(int pageNumber, int pageSize)
        {
            var categories = await _categoryProvider.GetByPageAsync(pageNumber, pageSize);
            var categoriesCount = await _categoryProvider.CountAsync();
            var result = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

            return PagedList<CategoryReadDto>.ToPagedList(pageNumber, pageSize, categoriesCount, result);
        }

        public async Task<CategoryReadDto> GetByIdAsync(int categoryId)
        {
            var category = await _categoryProvider.GetByIdAsync(categoryId);

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<int> AddAsync(CategoryAddDto model)
        {
            var newCategory = _mapper.Map<Category>(model);
            return await _categoryProvider.AddAsync(newCategory);
        }

        public async Task<int> UpdateAsync(CategoryUpdateDto model)
        {
            var modifiedCategory = _mapper.Map<Category>(model);
            return await _categoryProvider.UpdateAsync(modifiedCategory);
        }

        public async Task<int> DeleteAsync(int categoryId)
        {
            return await _categoryProvider.DeleteAsync(categoryId);
        }
    }
}
