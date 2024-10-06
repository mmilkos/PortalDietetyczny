using Flurl.Http;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Tokens;

public class GetCloudinarySettingsQuery : IRequest<OperationResult<CloudinarySettings>>
{
    
}

public class GetCloudinarySettingsQueryHandler : IRequestHandler<GetCloudinarySettingsQuery,OperationResult<CloudinarySettings>>
{
    private readonly IKeyService _keyService;
    
    public GetCloudinarySettingsQueryHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public async Task<OperationResult<CloudinarySettings>> Handle(GetCloudinarySettingsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CloudinarySettings>();
        
        var accessTokenDto = _keyService.GetVaultToken();
        
        var secrets =
            await
                "https://api.cloud.hashicorp.com/secrets/2023-06-13/organizations/0c4adf14-4d4f-4df0-bf0f-2b87ee2a0a1f/projects/88aa0b2e-1fb8-4721-91bf-f1d209b4d74a/apps/CloudinarySettings/open"
                    .WithOAuthBearerToken(accessTokenDto.Access_token)
                    .GetAsync(cancellationToken: cancellationToken)
                    .ReceiveJson<SecretResponseDto>();

        var apiKey = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.CloudinarySettings_ApiKey)
            ?.Version.Value;
        
        var apiSecret = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.CloudinarySettings_ApiSecret)
            ?.Version.Value;
        
        var cloudName = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.CloudinarySettings_CloudName)
            ?.Version.Value;

        if (apiKey.IsNullOrEmpty() || apiSecret.IsNullOrEmpty() || cloudName.IsNullOrEmpty())
        {
            operationResult.AddError("Błąd przy pobieraniu ustawień cloudinary");
            return operationResult;
        }
        
        var settings = new CloudinarySettings()
        {
            ApiKey = apiKey,
            ApiSecret = apiSecret,
            CloudName = cloudName
        };

        operationResult.Data = settings;

        return operationResult;
    }
}