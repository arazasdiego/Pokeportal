using System.ComponentModel.DataAnnotations;

namespace Pokeshop.Api.Models.Identity
{
    public class LoginVm
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
