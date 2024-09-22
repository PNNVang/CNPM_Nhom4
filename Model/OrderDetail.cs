namespace Dot_Net_ECommerceWeb.Model;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double QuantityTotal { get; set; } = 0;
    public double TotalPrice { get; set; }

    public Order Order { get; set; } // Navigation property
    public Product Product { get; set; } // Assuming you have a Product class
}