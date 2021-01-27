using System;

namespace Pokeshop.Entities.Entities
{
    public class ApplicationUser
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
