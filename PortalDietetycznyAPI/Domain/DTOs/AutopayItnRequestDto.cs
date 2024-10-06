namespace PortalDietetycznyAPI.DTOs;

public class AutopayItnRequestDto
{
    public string orderId { get; set; }
    public string transactionId { get; set; }
    public string status { get; set; }
    public int amount { get; set; }
    public string currency { get; set; }
    public string paymentChannel { get; set; }
    public string paymentStatusDetails { get; set; }
    public string customerIp { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string signature { get; set; }
}