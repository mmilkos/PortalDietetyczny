namespace PortalDietetycznyAPI.DTOs;

public class AutopayItnResponseDto
{
    public string ServiceID { get; set; }
    public string OrderID { get; set; }
    public string Confirmation { get; set; }
    public string Hash { get; set; }
}