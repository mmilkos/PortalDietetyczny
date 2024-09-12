namespace PortalDietetycznyAPI.Domain.Entities;

public class DietPhoto : Entity
{
    public string PublicId { get; set; }
    public string Url { get; set; }
    public Diet Diet { get; set; }
    public int? DietId { get; set; }
}