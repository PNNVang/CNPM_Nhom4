namespace Dot_Net_ECommerceWeb.Model;

public class Product
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public string ProductName { get; set; }
    public float? Price { get; set; }
    public string Status { get; set; }
    public int Sale { get; set; } = 0;
    public bool Hot { get; set; } = false;
    public string Description { get; set; }
    public string Information { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? ImageProduct { get; set; }
    public string StatusDeleted { get; set; }

    // Navigation Property for ProductImage
    public ProductImage ProductImage { get; set; }
}