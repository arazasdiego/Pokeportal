using Microsoft.AspNetCore.Builder;

namespace Pokeshop.Api.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokehub API");
                    options.RoutePrefix = string.Empty;
                });
    }
}
