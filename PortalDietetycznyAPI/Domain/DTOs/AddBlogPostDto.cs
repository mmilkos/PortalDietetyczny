namespace PortalDietetycznyAPI.DTOs;

public class AddBlogPostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string FileBytes { get; set; }
    public string FileName { get; set; }
}