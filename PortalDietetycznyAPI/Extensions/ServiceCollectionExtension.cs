using System.Reflection;
using System.Text;
using Dropbox.Api.Users;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Infrastructure.Context;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Application.Services;

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
        var connectionString = configuration.GetConnectionString("Database");
        var authentication = configuration.GetSection("Auth");
        var jwtIssuer = authentication.GetValue<string>("JwtIssuer");
        var jwtKey = authentication.GetValue<string>("JwtKey");
        
        services.AddSingleton<IKeyService,KeyService>();
        
        services.AddMediatR(typeof(AddIngredientCommand));
        
        services.AddHangfire(cfg =>
        {
            cfg.UseSqlServerStorage(connectionString);
        });
        
        services.AddHangfireServer();

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