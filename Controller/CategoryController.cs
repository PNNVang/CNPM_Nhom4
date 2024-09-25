using Dot_Net_ECommerceWeb.DBContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    public AppDBContext _context;

    public CategoryController(AppDBContext context)
    {
        _context = context;
    }

    [HttpGet("getcategories")]
    public async Task<IActionResult> getCategories()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }
}