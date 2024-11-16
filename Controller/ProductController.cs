using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDBContext _context;

    public ProductController(AppDBContext context)
    {
        _context = context;
    }

    // GET: api/product/getproduct_admin
    [HttpGet("getproduct_admin")]
    public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetProductImages()
    {
        var products = await _context.ProductImages
            .ToListAsync();
        // var productImages = products.Select(product => new ProductImageDTO
        // {
        //     Id = product.Id,
        //     ImgMain = product.ImgMain,
        //     Img1 = product.Img1,
        //     Img2 = product.Img2,
        //     Img3 = product.Img3,
        //     Img4 = product.Img4,
        // }).ToList();
        // Console.WriteLine(productImages);
        // foreach (var product in products)
        // {
        //     Console.WriteLine(product.ToString());
        // }
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

    // GET: api/product/redirect_update?id={id}
    [HttpGet("redirect_update")]
    public async Task<IActionResult> RedirectToUpdate([FromQuery] int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product); // Trả về sản phẩm để cập nhật
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
            // _context.Entry(product).Property(p =>p.CreatedAt).IsModified = true;
            // _context.Entry(product).Property(p =>p.Description).IsModified = true;
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        return BadRequest();
    }

    //PUT :api/product/updateproduct
    [HttpPut("updateproduct")]
    public async Task<String> UpdateProduct([FromBody] ProductDTO productDTO)
    {
        //kiểm tra từng trường dữ liệu gửi từ client xuống server có khác null hay không
        if (productDTO != null)
        {
            var products = _context.Products.Find(productDTO.Id);
            if (products != null)
            {
                products.Description= productDTO.Description;
                _context.Products.Update(products);
                _context.SaveChanges();
                return "Đã cập nhật thành công";
            }
        }

        return "Dữ liệu không được tìm thấy";
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
}