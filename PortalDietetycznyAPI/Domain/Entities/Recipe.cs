namespace PortalDietetycznyAPI.Domain.Entities;

public class Recipe : Entity
{
   public int Uid { get; set; }
   public List<RecipeTag> RecipeTags { get; set; } = [];
   public string Name { get; set; }
   public NutritionInfo Nutrition { get; set; } = new NutritionInfo();
   public List<RecipeIngredient> Ingredients { get; set; } = [];
   public string Instruction { get; set; }
   public int? PhotoId { get; set; }
   public RecipePhoto? Photo { get; set; }
   public string Url { get; set; }
}