using AutoMapper;
using Pokeshop.Api.Dtos.ProductDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<Product, ProductCartReadDto>();
            CreateMap<Product, ProductOrderReadDto>();
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
