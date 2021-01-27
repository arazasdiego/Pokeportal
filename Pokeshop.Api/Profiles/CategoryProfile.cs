using AutoMapper;
using Pokeshop.Api.Dtos.CategoryDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
