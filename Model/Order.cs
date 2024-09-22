namespace Dot_Net_ECommerceWeb.Model;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public float? TotalPrice { get; set; }
    public string Status { get; set; } // Use an enum if you prefer
    public int? UserId { get; set; }
    public string Address { get; set; }
    public string TypeShip { get; set; } // Use an enum if you prefer
    public string ShipCost { get; set; }
    public string TypePayment { get; set; } // Use an enum if you prefer
    public string StatusPayment { get; set; } // Use an enum if you prefer
    public string Note { get; set; }
    
    public ICollection<OrderDetail> OrderDetails { get; set; }
}