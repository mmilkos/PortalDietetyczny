namespace PortalDietetycznyAPI.DTOs;

public class ChangePasswordDto
{
    public string OldLogin { get; set; }
    public string OldPassword { get; set; }
    
    public string NewLogin { get; set; }
    public string NewPassword { get; set; }
}