namespace PortalDietetycznyAPI.DTOs;

public class SecretResponseDto
{
    public List<SecretDTO> Secrets { get; set; }
}

public class SecretDTO
{
    public string Name { get; set; }
    public SecretVersion Version { get; set; }
    public int CreatedById { get; set; }
}

public class SecretVersion
{
    public string Version { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}