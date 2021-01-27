using System.Linq;
using System.Security.Claims;

namespace Pokeshop.Api.Infrastructure
{
    public static class IdentityExtension
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            ?.Value;
    }
}
