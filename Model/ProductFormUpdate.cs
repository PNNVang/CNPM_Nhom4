using System.ComponentModel.DataAnnotations;

namespace Dot_Net_ECommerceWeb.Model;

public class ProductFormUpdate
{
    [Required]
    public string id { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "Tên sản phẩm không được quá 255 ký tự.")]
    public string productName { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Giá tiền phải là số không âm.")]
    public float price { get; set; }

    public IFormFile ImageMain { get; set; }
    public IFormFile Image1 { get; set; }
    public IFormFile Image2 { get; set; }
    public IFormFile Image3 { get; set; }
    public IFormFile Image4 { get; set; }

    [Required]
    public string status { get; set; } // "Còn hàng" hoặc "Hết hàng"

    [Range(0, 100, ErrorMessage = "Giảm giá phải từ 0 đến 100%.")]
    public int? sale { get; set; }

    [Required]
    public bool hot { get; set; } // 1 là có, 0 là không

    [StringLength(1000, ErrorMessage = "Mô tả không được quá 1000 ký tự.")]
    public string description { get; set; }
}