using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
{
    public AppDBContext _context;

    public CategoryController(AppDBContext context)
    {
        _context = context;
    }
    [HttpGet("getcategories")]
    public IActionResult GetCategories()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }
   
}