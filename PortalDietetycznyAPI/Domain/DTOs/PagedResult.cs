namespace PortalDietetycznyAPI.DTOs;

public class PagedResult<T>
{
    public int PageNumber { get; set; }
    public int TotalCount { get; set; }
    public List<T> Data { get; set; }
    public string Error { get; set; }
}