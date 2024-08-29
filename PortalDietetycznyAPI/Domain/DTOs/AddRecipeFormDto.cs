namespace PortalDietetycznyAPI.DTOs;

public class AddRecipeFormDto
{
    public string TagsIds { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string NutritionInfo { get; set; } 
    public string Instruction { get; set; }
    public IFormFile Photo { get; set; }
}