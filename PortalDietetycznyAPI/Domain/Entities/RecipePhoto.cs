namespace PortalDietetycznyAPI.Domain.Entities;

public class RecipePhoto : Entity
{
    public string PublicId { get; set; }
    public string Url { get; set; }
    public Recipe Recipe { get; set; }
    public int? RecipeId { get; set; }
}