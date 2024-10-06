using Flurl.Http;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Tokens;

public class GetAutopaySettingsQuery : IRequest<OperationResult<AutopaySettings>>
{
    
}

public class GetAutopaySettingsQueryHandler : IRequestHandler<GetAutopaySettingsQuery, OperationResult<AutopaySettings>>
{
    private readonly IKeyService _keyService;

    public GetAutopaySettingsQueryHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }

    public async Task<OperationResult<AutopaySettings>> Handle(GetAutopaySettingsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<AutopaySettings>();
        
        var accessTokenDto = _keyService.GetVaultToken();
        
        var secrets =
            await
                "https://api.cloud.hashicorp.com/secrets/2023-06-13/organizations/0c4adf14-4d4f-4df0-bf0f-2b87ee2a0a1f/projects/88aa0b2e-1fb8-4721-91bf-f1d209b4d74a/apps/AutopaySettings/open"
                    .WithOAuthBearerToken(accessTokenDto.Access_token)
                    .GetAsync(cancellationToken: cancellationToken)
                    .ReceiveJson<SecretResponseDto>();
        
        var apiKey = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.AutopaySettings_ApiKey)
            ?.Version.Value;
        
        var apiId = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.AutopaySettings_ApiId)
            ?.Version.Value;
        
        if (apiKey.IsNullOrEmpty() || apiId.IsNullOrEmpty())
        {
            operationResult.AddError("Błąd przy pobieraniu ustawień autopay");
            return operationResult;
        }

        var settings = new AutopaySettings()
        {
            ServiceID = apiId,
            ApiKey = apiKey
        };
        
        operationResult.Data = settings;
        return operationResult;
    }
}