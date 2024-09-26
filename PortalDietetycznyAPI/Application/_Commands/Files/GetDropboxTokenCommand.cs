using Dropbox.Api;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Files;

public class GetDropboxTokenCommand : IRequest<OperationResult<AccessTokenDto>>
{
    
}

public class GetDropboxTokenCommandHandler : IRequestHandler<GetDropboxTokenCommand, OperationResult<AccessTokenDto>>
{
    private readonly IKeyService _keyService;
    
    public GetDropboxTokenCommandHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public async Task<OperationResult<AccessTokenDto>> Handle(GetDropboxTokenCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<AccessTokenDto>();
        
        

        var accessTokenObject = _keyService.GetVaultToken();

        var accessToken = accessTokenObject.Access_token;
        
        var secrets = await "https://api.cloud.hashicorp.com/secrets/2023-06-13/organizations/0c4adf14-4d4f-4df0-bf0f-2b87ee2a0a1f/projects/88aa0b2e-1fb8-4721-91bf-f1d209b4d74a/apps/DropBoxSettings/open"
            .WithOAuthBearerToken(accessToken)
            .GetAsync(cancellationToken: cancellationToken)
            .ReceiveJson<SecretResponseDto>();

        var refreshToken = secrets.Secrets
            .First(secret => secret.Name == SecretsNamesRes.DropboxSettings_RefreshToken).Version.Value;
        
        var appKey = secrets.Secrets
            .First(secret => secret.Name == SecretsNamesRes.DropboxSettings_AppKey).Version.Value;
        
        var appSecret = secrets.Secrets
            .First(secret => secret.Name == SecretsNamesRes.DropboxSettings_AppSecret).Version.Value;
        
        var requestData = new 
        {
            grant_type = "refresh_token",
            refresh_token = refreshToken,
            client_id = appKey,
            client_secret = appSecret
        };

        var response = new AccessTokenDto();

        try
        { 
            response = await "https://api.dropboxapi.com/oauth2/token"
                .PostUrlEncodedAsync(requestData, cancellationToken: cancellationToken)
                .ReceiveJson<AccessTokenDto>();
        }
        catch (Exception e)
        {
            operationResult.AddError("Error");
            return operationResult;
        }

        operationResult.Data = response;

        return operationResult;
    }
}