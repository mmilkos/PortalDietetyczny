using System.ComponentModel.DataAnnotations;

namespace PortalDietetycznyAPI.DTOs;

public class OrderDto
{
    [Required()]
    [EmailAddress()]
    public string CustomerEmail { get; set; }
    [MinLength(1)]
    public List<int> ProductsIds { get; set; }
    public InvoiceDto? InvoiceDto { get; set; }
}