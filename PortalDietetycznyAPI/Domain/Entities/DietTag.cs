namespace PortalDietetycznyAPI.Domain.Entities;

public class DietTag
{
    public int DietId { get; set; }
    public Diet Diet { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}