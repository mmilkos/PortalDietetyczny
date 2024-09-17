using Flurl;
using Flurl.Http;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Tokens;

public class GetVaultTokenQuery : IRequest<OperationResult<AccessTokenDto>>
{
    
}

public class GetVaultTokenQueryHandler : IRequestHandler<GetVaultTokenQuery, OperationResult<AccessTokenDto>>
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    
    public GetVaultTokenQueryHandler()
    {
        _clientId = Environment.GetEnvironmentVariable("clientId");
        _clientSecret = Environment.GetEnvironmentVariable("clientSecret");
    }
    public async Task<OperationResult<AccessTokenDto>> Handle(GetVaultTokenQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<AccessTokenDto>();

        AccessTokenDto response;

        try
        {
            response = await "https://auth.idp.hashicorp.com"
                .AppendPathSegment("oauth2/token")
                .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                .PostUrlEncodedAsync(new {
                    client_id = _clientId,
                    client_secret = _clientSecret,
                    grant_type = "client_credentials",
                    audience = "https://api.hashicorp.cloud"
                }).ReceiveJson<AccessTokenDto>();
        }
        catch (Exception e)
        {
            operationResult.AddError("Błąd przy pobieraniu tokena z krypty");
            return operationResult;
        }

        operationResult.Data = response;

        return operationResult;
    }
}