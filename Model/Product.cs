using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Dot_Net_ECommerceWeb.Model;
[Table("products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int? Id { get; set; }
    [Column("category_id")]
    
    public int? CategoryId { get; set; }
    // public Category? Category { get; set; }
    // public string? ProductName { get; set; }
    // public float? Price { get; set; }
    // public string? Status { get; set; }
    // public int? Sale { get; set; } = 0;
    // public bool? Hot { get; set; } = false;
    [Column("description")]
    public string? Description { get; set; }
    // public string? Information { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    // public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
    
    // public int? ImageProduct { get; set; }
    // public string? StatusDeleted { get; set; }

    // Navigation Property for ProductImage
    public ProductImage? ProductImage { get; set; }
    public ICollection<OrderDetail> OrderDetail { get; set; } 
}