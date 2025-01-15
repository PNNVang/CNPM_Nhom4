using System.ComponentModel.DataAnnotations.Schema;

namespace Dot_Net_ECommerceWeb.DTO;
public class ProductImageDTO
{
    public int? Id { get; set; }
    public string? ImgMain { get; set; }
    public string? Img1 { get; set; }
    public string? Img2 { get; set; }
    public string? Img3 { get; set; }
    public string? Img4 { get; set; }
}