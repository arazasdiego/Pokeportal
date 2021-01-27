using AutoMapper;
using Pokeshop.Api.Dtos.CartDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartReadDto>();
            CreateMap<CartAddDto, Cart>();
            CreateMap<CartUpdateDto, Cart>();
        }
    }
}
