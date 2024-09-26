namespace PortalDietetycznyAPI.Domain.Entities;

public class Diet : Entity
{
    public string Name { get; set; }
    public int Kcal { get; set; }
    public int PhotoId { get; set; }
    public DietPhoto Photo { get; set; }
    public List<DietTag> DietTags { get; set; }
    public StoredFile File { get; set; }
    public int StoredFileId { get; set; }
    public int Price { get; set; }
}