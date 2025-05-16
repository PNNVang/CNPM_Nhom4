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
}