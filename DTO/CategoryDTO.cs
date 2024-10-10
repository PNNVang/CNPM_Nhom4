using Dot_Net_ECommerceWeb.Model;

namespace Dot_Net_ECommerceWeb.DTO;

public class CategoryNameDTO
{
    // public int Id { get; set; }
    public string CategoryName { get; set; }
    // public DateTime? CreatedAt { get; set; }
    // public DateTime? UpdatedAt { get; set; }
    // public DateTime? DeletedAt { get; set; }
    // public string Status { get; set; }
    // public string ImgLink { get; set; }
    public  ICollection<ProductDTO> Products { get; set; }
}