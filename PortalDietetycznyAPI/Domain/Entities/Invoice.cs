namespace PortalDietetycznyAPI.Domain.Entities;

public class Invoice : Entity
{
    public string? InvoiceId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime SaleDate { get; set; }
    public InvoiceParty Seller { get; set; }
    public InvoiceParty Buyer { get; set; }
    public List<Diet> Diets { get; set; }
    public int Amount { get; set; }
}