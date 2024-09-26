namespace PortalDietetycznyAPI.DTOs;

public class DietPreviewDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Kcal { get; set; }
    public string? PhotoUrl { get; set; }
    public decimal Price { get; set; }
}