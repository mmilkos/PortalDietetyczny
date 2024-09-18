using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Interfaces;

namespace PortalDietetycznyAPI.Application.Services;

public class PostConfigureService : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly IKeyService _keyService;
    
    public PostConfigureService(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        _keyService.GetVaultTokenJob();
        RecurringJob.AddOrUpdate(()=> _keyService.GetVaultTokenJob(), Cron.Hourly);

        var cloudinarySettings = _keyService.GetCloudinarySettingsAsync().Result;

        var portalSettings =  _keyService.GetPortalSettings().Result;
        
        
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = portalSettings.JwtIssuer,
            ValidAudience = portalSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(portalSettings.JwtKey))
        };
    }
}