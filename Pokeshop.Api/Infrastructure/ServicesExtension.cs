using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pokeshop.Api.Infrastructure.Services;
using Pokeshop.Api.Repositories;
using Pokeshop.Entities.Data;
using Pokeshop.Services;
using Pokeshop.Services.Dappers;
using System;
using System.Text;

namespace Pokeshop.Api.Infrastructure
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) =>
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pokeshop API",
                    Version = "v1"
                });
            });

        public static void AddApiControllers(this IServiceCollection services)
            => services
                .AddControllers()
                .AddNewtonsoftJson(options => options
                    .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        public static IServiceCollection AddSpecificCors(this IServiceCollection services)
        {
            return services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsConfig = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(appSettingsConfig);

            return appSettingsConfig.Get<AppSettings>();
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IContext, Context>()
                .AddTransient<ICurrentUserService, CurrentUserService>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<IIdentityRepository, IdentityRepository>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<ICartRepository, CartRepository>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ICartProvider, CartProvider>()
                .AddTransient<IOrderProvider, OrderProvider>()
                .AddTransient<IProductProvider, ProductProvider>()
                .AddTransient<IIdentityProvider, IdentityProvider>()
                .AddTransient<IOrderItemProvider, OrderItemProvider>()
                .AddTransient<IInvoiceProvider, InvoiceProvider>()
                .AddTransient<ICategoryProvider, CategoryProvider>();
        }
    }
}
