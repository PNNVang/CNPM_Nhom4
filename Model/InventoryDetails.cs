using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("inventory_detail")]
public class InventoryDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("price")]
    public float Price { get; set; }
    public Inventories Inventories { get; set; }
    public Product product { get; set; }
}