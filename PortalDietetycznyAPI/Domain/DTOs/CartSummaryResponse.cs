namespace PortalDietetycznyAPI.DTOs;

public class CartSummaryResponse
{
    public List<CartProduct> Products { get; set; }
    public int Total { get; set; }
}