using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("log")]
public class Logs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id { get; set; }
    [Column("ip_address")]
    public string? ip_address { get; set; }
    [Column("current_value")]
    public string? current_value { get; set; }
    [Column("created_at")]
    public DateTime created_at { get; set; }=DateTime.Now;
    
}