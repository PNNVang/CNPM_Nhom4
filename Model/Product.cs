using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Dot_Net_ECommerceWeb.Model;

[Table("products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("category_id")] public int? CategoryId { get; set; }

    [StringLength(50)]
    [Column("product_name")]
    public string? ProductName { get; set; }

    [Column("price")] public float Price { get; set; }
    [Column("status")] public string? Status { get; set; }
    [Column("sale")] public int Sale { get; set; } = 0; // Giá trị mặc định

    [Column("hot")] public int? Hot { get; set; } // Sử dụng bool thay vì tinyint
    // [Column("hot")] public bool Hot { get; set; } // Sử dụng bool thay vì tinyint
    [Column("description")] public string? Description { get; set; }

    [StringLength(200)]
    [Column("information")]
    public string? Information { get; set; } // NOT NULL

    [Column("created_at")] [JsonIgnore] public DateTime? CreatedAt { get; set; }
    [Column("updated_at")] [JsonIgnore] public DateTime? UpdatedAt { get; set; }
    [JsonIgnore] [Column("deleted_at")] public DateTime? DeletedAt { get; set; }
    [JsonIgnore] [Column("image_product")] public int? ImageProduct { get; set; }

    [JsonIgnore]
    [Column("status_deleted")]
    public string? StatusDeleted { get; set; }

    [JsonIgnore] public Category? Category { get; set; }

    // public Order? Order { get; set; }
    public List<OrderDetail>? OrderDetail { get; set; }
    [JsonIgnore] public ProductImage? ProductImage { get; set; }
    [JsonIgnore] public List<InventoryDetails>? InventoryDetail { get; set; }
}