namespace PortalDietetycznyAPI.DTOs;

public class AccessTokenDto
{
    public string Access_token { get; set; }
    public int Expires_in { get; set; }
    public string Token_type { get; set; }
}