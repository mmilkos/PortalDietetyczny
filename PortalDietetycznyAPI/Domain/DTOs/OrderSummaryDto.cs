namespace PortalDietetycznyAPI.DTOs;

public class OrderSummaryDto
{
    public string OrderId { get; set; }
    public int Amount { get; set; }
    public string CustomerEmail { get; set; }
    public string OrderStatus { get; set; }
    public string DietsNames { get; set; }
    public bool HasInvoice { get; set; }
    public int? InvoiceId { get; set; }
}