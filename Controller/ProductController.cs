using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("getproduct_admin")]
    public async Task<ActionResult> GetProduct()
    {
        var products = await _productService.GetProductAsync();
        return Ok(products);
    }

    [HttpDelete("deleteproduct_admin/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var isDeleted = await _productService.DeleteProductAsync(id);
        if (!isDeleted)
        {
            return NotFound(new { message = "Product not found" });
        }

        return Ok(new { message = "Product deleted successfully" });
    }

    [HttpPost("insertproduct")]
    public async Task<IActionResult> AddProduct(ProductForm model)
    {
        var isAdded = await _productService.AddProductAsync(model);
        if (!isAdded)
        {
            return BadRequest(new { message = "Failed to add product" });
        }

        return Ok(new { message = "Product added successfully" });
    }

    [HttpPost("updateproduct")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductFormUpdate model)
    {
        await _productService.UpdateProduct(model);
        return Ok(new { message = "Product updated successfully" });
    }
}