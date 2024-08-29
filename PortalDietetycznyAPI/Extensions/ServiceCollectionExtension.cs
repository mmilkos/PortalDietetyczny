using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Infrastructure.Context;
using PortalDietetycznyAPI.Infrastructure.Repositories;

namespace PortalDietetycznyAPI.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPDRepository, PdRepository>();
    }

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        const string bearer = "Bearer";
        var authentication = configuration.GetSection("Auth");
        var jwtIssuer = authentication.GetValue<string>("JwtIssuer");
        var jwtKey = authentication.GetValue<string>("JwtKey");

        services.AddMediatR(typeof(AddIngredientCommand));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddAuthentication(oprion =>
        {
            oprion.DefaultAuthenticateScheme = bearer;
            oprion.DefaultScheme = bearer;
            oprion.DefaultChallengeScheme = bearer;

        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
    }
}