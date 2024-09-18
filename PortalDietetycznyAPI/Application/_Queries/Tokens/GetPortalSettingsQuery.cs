using Flurl.Http;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Tokens;

public class GetPortalSettingsQuery : IRequest<OperationResult<PortalSettings>>
{
    
}

public class GetPortalSettingsQueryHandler : IRequestHandler<GetPortalSettingsQuery, OperationResult<PortalSettings>>
{
    private readonly IKeyService _keyService;
    
    public GetPortalSettingsQueryHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public async Task<OperationResult<PortalSettings>> Handle(GetPortalSettingsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<PortalSettings>();
        
        var accessTokenDto = _keyService.GetVaultToken();

        var secrets =
            await
                "https://api.cloud.hashicorp.com/secrets/2023-06-13/organizations/b80cc314-17b8-4aec-bfbb-292009a2c14b/projects/90c9452b-ef85-4be7-8a94-28be805e8bc1/apps/PortalSettings/open"
                    .WithOAuthBearerToken(accessTokenDto.Access_token)
                    .GetAsync(cancellationToken: cancellationToken)
                    .ReceiveJson<SecretResponseDto>();
        
        var jwtKey = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.PortalSettings_JwtKey)
            ?.Version.Value;
        
        var jwtIssuer = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.PortalSettings_JwtIssuer)
            ?.Version.Value;
        
        var jwtExpireHours = int.Parse(secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.PortalSettings_JwtExpireHours)
            ?.Version.Value) ;

        var settings = new PortalSettings()
        {
            JwtKey = jwtKey,
            JwtExpireHours = jwtExpireHours,
            JwtIssuer = jwtIssuer
        };

        operationResult.Data = settings;

        return operationResult;
    }
}