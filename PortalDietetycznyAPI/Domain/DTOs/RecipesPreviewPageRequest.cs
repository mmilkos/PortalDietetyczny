namespace PortalDietetycznyAPI.DTOs;

public class RecipesPreviewPageRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<int> TagsIds { get; set; } = [];
    public List<int> IngredientsIds { get; set; } = [];
}