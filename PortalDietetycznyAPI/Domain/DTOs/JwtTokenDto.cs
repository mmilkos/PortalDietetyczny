namespace PortalDietetycznyAPI.DTOs;

public class JwtTokenDto
{
    public string Token { get; set; }
    public CookieOptions CookieOptions { get; set; }
}