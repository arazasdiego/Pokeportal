using AutoMapper;
using Pokeshop.Api.Dtos.IdentityDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<ApplicationUser, IdentityReadDto>();
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<IdentityUpdateDto, ApplicationUser>();
        }
    }
}
