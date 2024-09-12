namespace PortalDietetycznyAPI.Domain.Entities;

public class BlogPost : Entity
{
    public int Uid { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int? PhotoId { get; set; }
    public BlogPhoto? Photo { get; set; }
    public string Url { get; set; }
}