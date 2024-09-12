namespace PortalDietetycznyAPI.Domain.Entities;

public class Tag : Entity
{
    public string Name { get; set; }
    public List<RecipeTag> RecipeTags { get; set; } = [];
    public List<DietTag> DietTags { get; set; } = [];
}