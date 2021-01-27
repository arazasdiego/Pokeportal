using AutoMapper;
using Pokeshop.Api.Dtos.OrderItemDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemReadDto>();
        }
    }
}
