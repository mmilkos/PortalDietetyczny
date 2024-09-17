namespace PortalDietetycznyAPI.DTOs;

public class FileDto
{
    public string FileName { get; set; }
    public MemoryStream Stream { get; set; }
    public string MimeType { get; set; }
}