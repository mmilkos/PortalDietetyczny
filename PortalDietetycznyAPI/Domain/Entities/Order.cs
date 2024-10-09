using PortalDietetycznyAPI.Domain.Enums;

namespace PortalDietetycznyAPI.Domain.Entities;

public class Order : Entity
{
    public string OrderId { get; set; }
    public int Amount { get; set; }
    public string CustomerEmail { get; set; }
    public string OrderStatus { get; set; }
    public List<StoredFile> StoredFiles { get; set; }
    public List<Diet> Diets { get; set; }
    public string DietsNames { get; set; }
    public bool HasInvoice { get; set; }
    public int? InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}