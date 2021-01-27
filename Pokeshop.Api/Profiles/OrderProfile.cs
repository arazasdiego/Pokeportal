using AutoMapper;
using Pokeshop.Api.Dtos.OrderDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDto>();
            CreateMap<Order, OrderFullReadDto>();
        }
    }
}
