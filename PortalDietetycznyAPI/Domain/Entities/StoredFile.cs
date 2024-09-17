using PortalDietetycznyAPI.Domain.Enums;

namespace PortalDietetycznyAPI.Domain.Entities;

public class StoredFile : Entity
{
    public string Name { get; set; }
    public string DropboxId { get; set; }
    public FileType FileType { get; set; }
}