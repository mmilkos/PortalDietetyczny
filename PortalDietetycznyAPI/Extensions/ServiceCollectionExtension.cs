using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Infrastructure.Context;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Application.Services;
using PortalDietetycznyAPI.Domain.Common;
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
        
        services.AddSingleton<IKeyService,KeyService>();

        services.Configure<HashicorpSettings>(configuration.GetSection("hashicorpSettings"));

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