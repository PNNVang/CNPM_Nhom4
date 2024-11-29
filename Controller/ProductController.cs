using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDBContext _context;
    private readonly Cloudinary _cloudinary;

    public ProductController(AppDBContext context, Cloudinary cloudinary)
    {
        _context = context;
        _cloudinary = cloudinary;
    }

//api lay thong tin san pham
    [HttpGet("getproduct_admin")]
    public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetProductImages()
    {
        var products = await _context.ProductImages
            .ToListAsync();

        return Ok(products);
    }

//api xoa san pham dua tren id
    [HttpDelete("deleteproduct_admin/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }


        var productEdited = new Product()
        {
            StatusDeleted = "Đã xóa"
        };
        _context.Products.Update(productEdited);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Product deleted successfully" });
    }

//api them san pham
    [HttpPost("insertproduct")]
    public async Task<IActionResult> AddProduct(ProductForm model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var products = new Product
        {
            CategoryId = model.categoryID,
            StatusDeleted = "chưa xóa",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Sale = 0,
            Id = _context.Products.Max(x => x.Id) + 1,
            Information = GenerateProductInfo(model),
            ProductName = model.productName,
            Price = (float?)model.price,
            Status = model.Status,
            Description = model.Description,
        };
        _context.Products.Add(products);
        await _context.SaveChangesAsync();
        var inventoryDetail = new InventoryDetails
        {
            Id = _context.InventoryDetails.Max(x => x.Id) + 1,
            Price = model.price,
            Quantity = model.NumberImport,
            ProductId = (from p in _context.Products
                where p.ProductName == model.productName
                select p.Id).FirstOrDefault()
        };
        var inventory = new Inventories
        {
            id = _context.Inventories.Max(x => x.id) + 1,
            date = DateTime.Now,
            user_imported = "admin"
        };
        _context.InventoryDetails.Add(inventoryDetail);
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();
        var imageUrls = await UploadImagesToCloudinary(model);
        var productImages = new ProductImage
        {
            ImgMain = imageUrls[0],
            Img1 = imageUrls[1],
            Img2 = imageUrls[2],
            Img3 = imageUrls[3],
            Img4 = imageUrls[4]
        };
        _context.ProductImages.Add(productImages);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Product added successfully" });
    }
//hàm upload nhiều ảnh lên cloudinary 
    private async Task<string[]> UploadImagesToCloudinary(ProductForm model)
    {
        var imageUrls = new string[5]; // Mảng chứa các URL ảnh (5 ảnh)

        // Mảng ảnh cần upload
        var images = new IFormFile[] { model.MainImage, model.Image1, model.Image2, model.Image3, model.Image4 };

        for (int i = 0; i < images.Length; i++)
        {
            var image = images[i];
            if (image != null)
            {
                var uploadResult = await UploadImageToCloudinary(image);
                if (uploadResult != null)
                {
                    imageUrls[i] = uploadResult.Url.ToString(); // Lưu URL ảnh vào mảng
                }
            }
        }

        return imageUrls;
    }
//hàm upload ảnh lên cloudinary
    private async Task<ImageUploadResult> UploadImageToCloudinary(IFormFile image)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(image.FileName, image.OpenReadStream())
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult;
    }
//hàm tạo thông tin
    public string GenerateProductInfo(ProductForm model)
    {
        var productInfo = $"/color:{model.Color};" +
                          $"weight:{model.Weight};" +
                          $"size:{model.Size};" +
                          $"opacity:{model.Opacity};" +
                          $"cutting_form:{model.CuttingForm}/";

        return productInfo;
    }

//api lấy thông tin sản phẩm cho viec cap nhat san pham
    [HttpGet("getproduct/{id}")]
    public async Task<IActionResult> getProduct(int id)
    {
        var products = from product in _context.Products
            select new
            {
                product.Id,
            };
        return Ok(products);
    }

//api cập nhật sản phẩm
    [HttpPut("updateproduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductFormUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        if (model.productName != null)
        {
            product.ProductName = model.productName;
            _context.Products.Update(product);
        }

        if (model.price != null)
        {
            product.Price = (float?)model.price;
            _context.Products.Update(product);
        }

        if (model.status != null)
        {
            product.Status = model.status;
            _context.Products.Update(product);
        }

        if (model.description != null)
        {
            product.Description = model.description;
            _context.Products.Update(product);
        }

        if (model.ImageMain != null)
        {
            var uploadResult = await UploadImageToCloudinary(model.ImageMain);
            var productImageMain = uploadResult.Url.ToString();
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound(new { message = "Product Image not found" });
            }

            productImage.ImgMain = productImageMain;
            _context.ProductImages.Update(productImage);
        }

        if (model.Image1 != null)
        {
            var uploadResult = await UploadImageToCloudinary(model.Image1);
            var productImage1 = uploadResult.Url.ToString();
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound(new { message = "Product Image not found" });
            }

            productImage.Img1 = productImage1;
            _context.ProductImages.Update(productImage);
        }

        if (model.Image2 != null)
        {
            var uploadResult = await UploadImageToCloudinary(model.Image2);
            var productImage2 = uploadResult.Url.ToString();
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound(new { message = "Product Image not found" });
            }

            productImage.Img2 = productImage2;
            _context.ProductImages.Update(productImage);
        }

        if (model.Image3 != null)
        {
            var uploadResult = await UploadImageToCloudinary(model.Image3);
            var productImage3 = uploadResult.Url.ToString();
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound(new { message = "Product Image not found" });
            }

            productImage.Img3 = productImage3;
            _context.ProductImages.Update(productImage);
        }

        if (model.Image4 != null)
        {
            var uploadResult = await UploadImageToCloudinary(model.Image4);
            var productImage4 = uploadResult.Url.ToString();
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound(new { message = "Product Image not found" });
            }

            productImage.Img4 = productImage4;
            _context.ProductImages.Update(productImage);
        }

        await _context.SaveChangesAsync();
        return Ok("Update successfully");
    }
}