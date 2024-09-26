namespace PortalDietetycznyAPI.DTOs;

public class DietsPreviewPageRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<int> TagsIds { get; set; } = [];
}