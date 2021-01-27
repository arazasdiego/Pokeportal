using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Dtos.IdentityDtos
{
    public class IdentityUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
