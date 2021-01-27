using Microsoft.Extensions.Configuration;

namespace Pokeshop.Api.Infrastructure
{
    public static class ConfigurationExtension
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
