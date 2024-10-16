﻿using MediatR;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Application._Queries.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application.Services;

public class KeyService : IKeyService
{
    private static AccessTokenDto? _VaultToken;
    
    private static AccessTokenDto? _dropboxToken;
    private static DateTime? _dropboxTokenExpirationDate;

    private static CloudinarySettings? _cloudinarySettings;
    private static PortalSettings? _portalSettings;
    private static AutopaySettings? _autopaySettings;

    private readonly IMediator _mediator;

    public KeyService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void GetVaultTokenJob()
    {
        var vaultToken = GetVaultTokenAsync().Result;
        _VaultToken = vaultToken;
    }
    
    public AccessTokenDto GetVaultToken()
    {
        return _VaultToken;
    }

    private async Task<AccessTokenDto> GetVaultTokenAsync()
    {
        var result = await _mediator.Send(new GetVaultTokenQuery());
        return result.Data;
    }

    public async Task<AccessTokenDto> GetDropBoxToken()
    {
        var result = new OperationResult<AccessTokenDto>();

        if (_dropboxToken == null)
        {
            result = await _mediator.Send(new GetDropboxTokenCommand());
            _dropboxToken = result.Data;
            _dropboxTokenExpirationDate = DateTime.UtcNow.AddSeconds(result.Data.Expires_in);

            return _dropboxToken;
        }

        if (!(DateTime.Now >= _dropboxTokenExpirationDate)) return _dropboxToken;
        
        result = await _mediator.Send(new GetDropboxTokenCommand());
        _dropboxToken = result.Data;
        _dropboxTokenExpirationDate = DateTime.UtcNow.AddSeconds(result.Data.Expires_in);

        return _dropboxToken;
    }

    public async Task<CloudinarySettings> GetCloudinarySettingsAsync()
    {
        OperationResult<CloudinarySettings> result;

        if (_cloudinarySettings != null) return _cloudinarySettings;
        
        result = await _mediator.Send(new GetCloudinarySettingsQuery());
        _cloudinarySettings = result.Data;

        return _cloudinarySettings;
    }

    public async Task<PortalSettings> GetPortalSettings()
    {
        OperationResult<PortalSettings> result;

        if (_portalSettings != null) return  _portalSettings;
        
        result = await _mediator.Send(new GetPortalSettingsQuery());
        _portalSettings = result.Data;

        return _portalSettings;
    }

    public async Task<AutopaySettings> GetAutopaySettings()
    {
        OperationResult<AutopaySettings> result;

        if (_autopaySettings != null) return _autopaySettings;

        result = await _mediator.Send(new GetAutopaySettingsQuery());
        _autopaySettings = result.Data;

        return _autopaySettings;
    }
}