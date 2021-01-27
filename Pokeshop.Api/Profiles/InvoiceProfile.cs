using AutoMapper;
using Pokeshop.Api.Dtos.InvoiceDtos;
using Pokeshop.Entities.Entities;

namespace Pokeshop.Api.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceReadDto>();
        }
    }
}
