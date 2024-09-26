using Flurl;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Tokens;

public class GetVaultTokenQuery : IRequest<OperationResult<AccessTokenDto>>
{
    
}

public class GetVaultTokenQueryHandler : IRequestHandler<GetVaultTokenQuery, OperationResult<AccessTokenDto>>
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly HashicorpSettings _settings;
    
    public GetVaultTokenQueryHandler(IOptions<HashicorpSettings> settings)
    {
        _clientId = Environment.GetEnvironmentVariable("clientId");
        _clientSecret = Environment.GetEnvironmentVariable("clientSecret");
        _settings = settings.Value;
    }
    public async Task<OperationResult<AccessTokenDto>> Handle(GetVaultTokenQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<AccessTokenDto>();

        AccessTokenDto response;

        try
        {
            response = await _settings.Url
                .AppendPathSegment("oauth2/token")
                .WithHeader(_settings.headerName, _settings.headerValue)
                .PostUrlEncodedAsync(new {
                    client_id = _clientId,
                    client_secret = _clientSecret,
                    grant_type = _settings.GrantType,
                    audience = _settings.Audience
                }, cancellationToken: cancellationToken).ReceiveJson<AccessTokenDto>();
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.TokenError);
            return operationResult;
        }

        operationResult.Data = response;

        return operationResult;
    }
}