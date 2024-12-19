using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("inventories")]
public class Inventories
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id { get; set; }
    [Column("date")]
    public DateTime date { get; set; }
    [Column("user_imported")]
    [StringLength(50)]
    public string user_imported { get; set; }
    public List<InventoryDetails>? inventory_details { get; set; }
}