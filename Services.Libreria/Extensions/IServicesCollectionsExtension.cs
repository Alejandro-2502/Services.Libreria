using AutoMapper;
using Libreria.Application.Configuration;
using Libreria.Application.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Services.Libreria.Extensions;

public static class IServicesCollectionsExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
    {
        ConfigHelper.ConfigSqlServer = configuration.GetSection(nameof(ConfigSqlServer)).Get<ConfigSqlServer>();
        ConfigHelper.ConfigJwt = configuration.GetSection(nameof(ConfigJwt)).Get<ConfigJwt>();
        ConfigHelper.ServerRedis = configuration.GetSection(nameof(ServerRedis)).Get<ServerRedis>();
        ConfigHelper.TTLCaches = configuration.GetSection(nameof(TTLCacheRedis)).Get<TTLCacheRedis>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        AddConfigureSwagger(services);
        services.AddMvc();
        services.AddInjection();
        services.AddAutoMapper();
        AddAuthentication(services);

        return services;
    }

     public static IServiceCollection AddConfigureSwagger(this IServiceCollection services)
     {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "Servicio Libreria", Version = "v1" });
            });

            return services;
     }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(typeof(MapperProfile));
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = ConfigHelper.ConfigJwt.Issuer,
                ValidAudience = ConfigHelper.ConfigJwt.Audience,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.ConfigJwt.Key))
            };
        });

        return services;
    }

    public static IServiceCollection AddStackExchangeRedisCache(this IServiceCollection services)
    {

        services.AddStackExchangeRedisCache(option =>
        {
            option.Configuration = ConfigHelper.ServerRedis!.Localhost;
        });

        return services;
    }
}
