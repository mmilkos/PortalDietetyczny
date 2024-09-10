using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.DTOs;

public class RecipeDetailsDto
{
    public int Uid { get; set; }
    public List<string> RecipeTags { get; set; } = [];
    public string Name { get; set; }
    public NutritionInfo Nutrition { get; set; } = new NutritionInfo();
    public List<RecipeIngredientDto> Ingredients { get; set; } = [];
    public string Instruction { get; set; }
    public string? PhotoUrl { get; set; }
}