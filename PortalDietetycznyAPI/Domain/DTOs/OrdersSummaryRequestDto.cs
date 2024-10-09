namespace PortalDietetycznyAPI.DTOs;

public class OrdersSummaryRequestDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? DateFrom { get; set; }
    public string? DateTo { get; set; }
}