namespace PortalDietetycznyAPI.Domain.Common;

public class PortalSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireHours { get; set; }
    public string JwtIssuer { get; set; }
}