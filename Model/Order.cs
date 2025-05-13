using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("orders")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("total_price")]
    public float? TotalPrice { get; set; }
    [Column("status")]
    public string? Status { get; set; } // Use an enum if you prefer
    [Column("user_id")]
    public int? UserId { get; set; }
    [Column("address")]
    public string? Address { get; set; }
    [Column("typeShip")]
    public string? TypeShip { get; set; } // Use an enum if you prefer
    [Column("shipCost")]
    public string? ShipCost { get; set; }
    [Column("typePayment")]
    public string? TypePayment { get; set; } // Use an enum if you prefer
    [Column("statusPayment")]
    public string? StatusPayment { get; set; } // Use an enum if you prefer
    [Column("note")]
    public string? Note { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }

    [ForeignKey("UserId")]
    public User user { get; set; }
}