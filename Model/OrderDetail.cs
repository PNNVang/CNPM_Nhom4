using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("order_details")]
public class OrderDetail
{
    [Key]
    [Column("order_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? OrderId { get; set; }
    [Column("product_id")]
    public int? ProductId { get; set; }
    [Column("quantity_total")]
    public double? QuantityTotal { get; set; } 
    [Column("total_price")]
    public double? TotalPrice { get; set; }

    public Order? Order { get; set; } // Navigation property
    public Product? Product { get; set; } // Assuming you have a Product class
}