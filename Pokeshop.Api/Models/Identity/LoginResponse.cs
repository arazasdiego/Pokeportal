namespace Pokeshop.Api.Models.Identity
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool Success { get; set; } = false;
        public string Secret { get; set; }
    }
}
