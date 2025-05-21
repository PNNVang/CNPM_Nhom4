using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

[Route("/api/[controller]")]
[ApiController]
public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ProductManagementService _productService;

    public ProductController(ProductManagementService productService)
    {
        _productService = productService;
    }

    [HttpGet("getproduct_admin")]
    public async Task<ActionResult> GetProduct()
    {
        var products = await _productService.GetProductAsync();
        return Ok(products);
    }

    //6.1: Admin gửi yêu cầu thêm sản phẩm mới
    [HttpPost("insertproduct")]
    public async Task<IActionResult> AddProduct(ProductForm model)
    {
        var isAdded = await _productService.AddProductAsync(model);
        if (!isAdded)
        {
            //6.1.7: Thêm không thành công sản phẩm 
            return BadRequest(new { message = "Failed to add product" });
        }

        //6.1.6: Thêm thành công sản phẩm 
        return Ok(new { message = "Product added successfully" });
    }
}