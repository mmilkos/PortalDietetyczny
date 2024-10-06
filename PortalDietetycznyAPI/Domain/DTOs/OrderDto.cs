namespace PortalDietetycznyAPI.DTOs;

public class OrderDto
{
    public string CustomerEmail { get; set; }
    public List<int> ProductsIds { get; set; }
    public InvoiceDto? InvoiceDto { get; set; }
}