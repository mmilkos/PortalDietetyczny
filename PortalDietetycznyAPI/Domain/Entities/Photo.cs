namespace PortalDietetycznyAPI.Domain.Entities;

public class Photo : Entity
{
    public string PublicId { get; set; }
    public string Url { get; set; }
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}