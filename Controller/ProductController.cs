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
    private readonly CloudinaryService _cloudinaryService;

    public ProductController(AppDBContext context, CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    // GET: api/product/getproduct_admin
    [HttpGet("getproduct_admin")]
    public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetProductImages()
    {
        var products = await _context.ProductImages
            .ToListAsync();
     
        return Ok(products);
    }

    // DELETE: api/product/deleteproduct_admin?id={id}
    [HttpDelete("deleteproduct_admin")]
    public async Task<IActionResult> DeleteProduct([FromQuery] int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        // Xóa hình ảnh liên quan nếu cần
        var images = await _context.ProductImages.Where(img => img.Id == id).ToListAsync();
        _context.ProductImages.RemoveRange(images);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Product deleted successfully" });
    }



    //POST: api/product/insertproduct
    [HttpPost("insertproduct")]
    public async Task<IActionResult> InsertProduct([FromBody] ProductDTO productDTO)
    {
        //kiểm tra từng thông tin của sản phẩm trước khi lưu xuống DB
        if (productDTO != null)
        {
            var product = new Product
            {
                Id = productDTO.Id,
                CreatedAt = productDTO.CreatedAt,
                Description = productDTO.Description
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        return BadRequest();
    }


    [HttpGet("getproduct")]
    public async Task<IActionResult> getProduct()
    {
        var products = from product in _context.Products
            join category in _context.Categories
                on product.CategoryId equals category.Id
            select new
            {
                product.Id,
            };
        return Ok(products);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] List<IFormFile> file)
    {
        List<string> imageUrlList = new List<string>();
        if (file == null || file.Count < 0)
        {
            return BadRequest("No file uploaded.");
        }

        foreach (var fileObject in file)
        {
            var urlFolder = "asp image";
            var imageUrl = await _cloudinaryService.UploadImageAsync(fileObject, urlFolder);
            if (imageUrl == null)
            {
                return StatusCode(500, "Failed to upload image.");
            }

            var imageURLJSON = new { Url = imageUrl };
            imageUrlList.Add(imageURLJSON.Url);
        }


        return Ok(new { imgUrlList = imageUrlList });
    }

    [HttpPut("updateproduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        product.Description = productDTO.Description;
        _context.Products.Update(product);
        return Ok(product);
    }
}