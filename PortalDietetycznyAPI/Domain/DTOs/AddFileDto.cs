using PortalDietetycznyAPI.Domain.Enums;

namespace PortalDietetycznyAPI.DTOs;

public class AddFileDto
{
    public string Name { get; set; }
    public string FileBytes { get; set; }
    public string FileName { get;  set; }

    public FileType FileType { get; set; }
}