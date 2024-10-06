namespace PortalDietetycznyAPI.DTOs;

public class PaymentRequest
{
    public string ServiceID { get; set; }
    public string OrderID { get; set; }
    public int Amount { get; set; }
    public string CustomerEmail { get; set; }
    public string ValidityTime { get; set; }
    public string LinkValidityTime { get; set; }
    public string Hash { get; set; }
}