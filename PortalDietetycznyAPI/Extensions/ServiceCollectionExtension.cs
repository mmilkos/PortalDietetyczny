using System.Reflection;
using System.Text;
using Dropbox.Api.Users;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Infrastructure.Context;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Application.Services;
using PortalDietetycznyAPI.Infrastructure.Seeders;

namespace PortalDietetycznyAPI.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPDRepository, PdRepository>();

        services.AddScoped<AccountSeeder>();
    }

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        const string bearer = "Bearer";
        var connectionString = configuration.GetConnectionString("Database");
        var authentication = configuration.GetSection("Auth");
        var jwtIssuer = authentication.GetValue<string>("JwtIssuer");
        var jwtKey = authentication.GetValue<string>("JwtKey");
        
        services.AddSingleton<IKeyService,KeyService>();

        services.AddTransient<IPostConfigureOptions<JwtBearerOptions>, PostConfigureService>();
        
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

        })
        .AddCookie(cookie =>
        {
            cookie.Cookie.Name = "token";
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["token"];
                    return Task.CompletedTask;
                }
            };

        });
    }
}