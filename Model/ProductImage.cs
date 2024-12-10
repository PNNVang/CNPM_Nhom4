using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.Model;
[Table("product_image")]

public class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int? Id { get; set; }
    [Column("img_main")]
    public string? ImgMain { get; set; }
    [Column("img_1")]
    public string? Img1 { get; set; }
    [Column("img_2")]
    public string? Img2 { get; set; }
    [Column("img_3")]
    public string? Img3 { get; set; }
    [Column("img_4")]
    public string? Img4 { get; set; }
    
    // Navigation Property for Product
    public Product Product { get; set; }
}