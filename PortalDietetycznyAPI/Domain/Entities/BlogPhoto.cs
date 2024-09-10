namespace PortalDietetycznyAPI.Domain.Entities;

public class BlogPhoto : Entity
{
    public string PublicId { get; set; }
    public string Url { get; set; }
    public BlogPost BlogPost { get; set; }
    public int? BlogPostId { get; set; }
}