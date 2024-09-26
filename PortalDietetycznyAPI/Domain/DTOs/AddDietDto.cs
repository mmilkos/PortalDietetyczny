namespace PortalDietetycznyAPI.DTOs;

public class AddDietDto
{
    public List<int> TagsIds { get; set; }
    public string Name { get; set; }
    public int Kcal { get; set; }
    public string FileBytes { get; set; }
    public string FileName { get; set; }
    public string PhotoFileBytes { get; set; }
    public string PhotoFileName { get; set; }
    public int Price { get; set; }
}