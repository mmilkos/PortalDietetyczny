using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Domain.Interfaces;

public interface IKeyService
{
    Task<AccessTokenDto> GetDropBoxToken();
    AccessTokenDto GetVaultToken();
    void GetVaultTokenJob();
    Task<CloudinarySettings> GetCloudinarySettingsAsync();
}