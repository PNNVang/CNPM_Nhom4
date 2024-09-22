using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("api/[controller]")]
[ApiController]
public class ProductController:ControllerBase
{
    private readonly AppDBContext _context;

    public ProductController(AppDBContext context)
    {
        _context = context;
    }

    // GET: api/product/getproduct_admin
    [HttpGet("getproduct_admin")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products
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

    // GET: api/product/redirect_update?id={id}
    [HttpGet("redirect_update")]
    public async Task<IActionResult> RedirectToUpdate([FromQuery] int id)
    {
        var product = await _context.Products.Include(p => p.ProductImage)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product); // Trả về sản phẩm để cập nhật
    }
}