using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;

[ApiController]
[Route("/api/[controller]")]
public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
{
   public CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
      _categoryService = categoryService;  
    }
    [HttpGet("getcategories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        return Ok(categories);
    }
   
}