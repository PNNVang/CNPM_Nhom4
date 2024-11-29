using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Model;

public class ProductForm
{
    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [Display(Name = "Tên sản phẩm")]
    public string productName { get; set; }
    [Required(ErrorMessage = "Không được phép để trống số liệu")]
    public int categoryID { get; set; }
    [Required(ErrorMessage = "Giá tiền không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá tiền phải là số dương")]
    [Display(Name = "Giá tiền")]
    public float price { get; set; }

    [Display(Name = "Ảnh chính")]
    [BindProperty(Name = "image-main")]
    public IFormFile MainImage { get; set; }

    [Display(Name = "Ảnh phụ 1")]
    [BindProperty(Name="image-1")]
    public IFormFile Image1 { get; set; }

    [Display(Name = "Ảnh phụ 2")]
    [BindProperty(Name="image-2")]
    public IFormFile Image2 { get; set; }

    [Display(Name = "Ảnh phụ 3")]
    [BindProperty(Name="image-3")]
    public IFormFile Image3 { get; set; }

    [Display(Name = "Ảnh phụ 4")]
    [BindProperty(Name="image-4")]
    public IFormFile Image4 { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số dương")]
    [Display(Name = "Số lượng")]
    [BindProperty(Name = "number_import")]
    public int NumberImport { get; set; }

    [Display(Name = "Màu sắc")]
    [BindProperty(Name="color")]
    public string Color { get; set; }

    [Display(Name = "Trọng lượng")]
    [BindProperty(Name="weight")]
    public string Weight { get; set; }

    [Display(Name = "Kích cỡ")]
    [BindProperty(Name="size")]
    public string Size { get; set; }

    [Display(Name = "Độ trong")]
    [BindProperty(Name="opacity")]
    public string Opacity { get; set; }

    [Display(Name = "Giác cắt")]
    [BindProperty(Name="cutting_form")]
    public string CuttingForm { get; set; }

    [Display(Name = "Tình trạng")]
    [BindProperty(Name="status")]
    public string Status { get; set; }

    [Display(Name = "Mô tả sản phẩm")]
    [BindProperty(Name="description")]
    public string Description { get; set; }
}