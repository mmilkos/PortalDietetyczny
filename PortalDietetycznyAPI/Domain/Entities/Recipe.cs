namespace PortalDietetycznyAPI.Domain.Entities;

public class Recipe : Entity
{
   public List<RecipeTag> RecipeTags { get; set; } = [];
   public string Name { get; set; }
   public NutritionInfo Nutrition { get; set; } = new NutritionInfo();
   public List<RecipeIngredient> Ingredients { get; set; } = [];
   public string Instruction { get; set; }
   public int? PhotoId { get; set; }
   public Photo? Photo { get; set; }
}