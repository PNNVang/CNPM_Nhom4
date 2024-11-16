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
    [StringLength(50)]
    [Column("product_name")]
    public string? ProductName { get; set; }
    [Column("price")]
    public float? Price { get; set; }
    [Column("status")]
    public string? Status { get; set; }
    [Column("sale")]
    public int Sale { get; set; } = 0; // Giá trị mặc định
    [Column("hot")]
    public bool Hot { get; set; } = false; // Sử dụng bool thay vì tinyint
    [Column("description")]
    public string? Description { get; set; }
    [StringLength(200)]
    [Column("information")]
    public string? Information { get; set; } // NOT NULL
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
    [Column("image_product")]
    public int? ImageProduct { get; set; }
    [Column("status_deleted")]
    public string? StatusDeleted { get; set; }
    public Category? Category { get; set; }
    public Order? Order { get; set; }
}