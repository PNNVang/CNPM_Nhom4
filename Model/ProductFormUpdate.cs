using System.ComponentModel.DataAnnotations;

namespace Dot_Net_ECommerceWeb.Model;

public class ProductFormUpdate
{
    [Required]
    public int id { get; set; }
    [StringLength(255, ErrorMessage = "Tên sản phẩm không được quá 255 ký tự.")]
    public string? productName { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Giá tiền phải là số không âm.")]
    public float? price { get; set; }

    public IFormFile? ImageMain { get; set; }
    public IFormFile? Image1 { get; set; }
    public IFormFile? Image2 { get; set; }
    public IFormFile? Image3 { get; set; }
    public IFormFile? Image4 { get; set; }
    
    public string? status { get; set; } // "Còn hàng" hoặc "Hết hàng"

 
    public int? sale { get; set; }
    
    public bool? hot { get; set; } // 1 là có, 0 là không

   
    public string? description { get; set; }
}