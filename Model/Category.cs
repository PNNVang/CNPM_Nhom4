using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("categories")]
public class Category
{
 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }
        [Column("category_name")]
        public string? CategoryName { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("img_link")]
        public string? ImgLink { get; set; }
        public  ICollection<Product>? Products { get; set; }
    }
