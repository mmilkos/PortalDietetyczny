namespace PortalDietetycznyAPI.Domain.Entities;

public class RecipeIngredient : Entity
{
    public int RecipeId { get; set; }
    
    public Ingredient Ingredient { get; set; }
    
    public int IngredientId { get; set; }

    public string Unit { get; set; }
    public float UnitValue { get; set; }
    public string HomeUnit { get; set; }
    public float HomeUnitValue { get; set; }
}