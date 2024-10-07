namespace PortalDietetycznyAPI.Domain.Common;

public class PortalSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireHours { get; set; }
    public string JwtIssuer { get; set; }
    public string Mail { get; set; }
    public string MailSecret { get; set; }
}