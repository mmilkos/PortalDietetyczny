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
                "https://api.cloud.hashicorp.com/secrets/2023-06-13/organizations/0c4adf14-4d4f-4df0-bf0f-2b87ee2a0a1f/projects/88aa0b2e-1fb8-4721-91bf-f1d209b4d74a/apps/PortalSettings/open"
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
            ?.Version.Value);
        
        var mail = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.PortalSettings_Mail)
            ?.Version.Value;
        
        var mailSecret = secrets.Secrets
            .FirstOrDefault(secret => secret.Name == SecretsNamesRes.PortalSettings_MailSecret)
            ?.Version.Value;

        var settings = new PortalSettings()
        {
            JwtKey = jwtKey,
            JwtExpireHours = jwtExpireHours,
            JwtIssuer = jwtIssuer,
            MailSecret = mailSecret,
            Mail = mail
        };

        operationResult.Data = settings;

        return operationResult;
    }
}