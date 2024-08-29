using PortalDietetycznyAPI.Domain.Entities;

namespace PortalDietetycznyAPI.DTOs;

public class AddRecipeDto
{
    public List<int> TagsIds { get; set; }
    public string Name { get; set; }
    public List<RecipeIngredientDto> Ingredients { get; set; }
    public NutritionInfo NutritionInfo { get; set; } 
    public string Instruction { get; set; }
    public IFormFile? File { get; set; }
}