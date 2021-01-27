using System;

namespace Pokeshop.Api.Dtos.IdentityDtos
{
    public class IdentityReadDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }      
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
