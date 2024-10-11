using Hangfire;
using Hangfire.PostgreSql;
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
using PortalDietetycznyAPI.Domain.Entities;
namespace PortalDietetycznyAPI.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    { 
        var connection = Environment.GetEnvironmentVariable("connection");
        
        services.AddDbContext<Db>(options => options.UseNpgsql(connection));
        services.AddScoped<IPDRepository, PdRepository>();
    }

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        const string bearer = "Bearer";
        const string token = "token";
        
        Microsoft.Playwright.Program.Main(["install"]);
        
        services.AddSingleton<IKeyService,KeyService>();
        services.AddScoped<IEmailService,EmailService>();

        services.Configure<HashicorpSettings>(configuration.GetSection("hashicorpSettings"));

        services.AddTransient<IPostConfigureOptions<JwtBearerOptions>, PostConfigureService>();
        
        services.AddMediatR(typeof(AddIngredientCommand));
        
        var connection = Environment.GetEnvironmentVariable("connection");
        
        services.AddHangfire(cfg =>
        {
            cfg.UsePostgreSqlStorage(connection);
        });
        
        services.AddHangfireServer();
        
        services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<Db>(); 

        services.AddAuthentication(oprion =>
        {
            oprion.DefaultAuthenticateScheme = bearer;
            oprion.DefaultScheme = bearer;
            oprion.DefaultChallengeScheme = bearer;
        })
        .AddCookie(cookie =>
        {
            cookie.Cookie.Name = token;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[token];
                    return Task.CompletedTask;
                }
            };
        });
    }
}