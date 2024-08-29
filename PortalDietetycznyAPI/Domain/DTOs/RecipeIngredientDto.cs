namespace PortalDietetycznyAPI.DTOs;

public class RecipeIngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public float UnitValue { get; set; }
    public string HomeUnit { get; set; }
    public float HomeUnitValue { get; set; }
}