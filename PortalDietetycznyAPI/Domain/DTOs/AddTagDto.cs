using PortalDietetycznyAPI.Domain.Common;

namespace PortalDietetycznyAPI.DTOs;

public class AddTagDto
{
    public string Name { get; set; }
    public TagContext Context { get; set; }
}