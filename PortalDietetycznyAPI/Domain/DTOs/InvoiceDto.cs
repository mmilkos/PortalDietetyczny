using System.ComponentModel.DataAnnotations;

namespace PortalDietetycznyAPI.DTOs;

public class InvoiceDto
{
    [Required()]
    public string Name { get; set; }
    [Required()]
    public string LastName { get; set; }
    [Required()]
    public string Street { get; set; }
    [Required()]
    public string City { get; set; }
}