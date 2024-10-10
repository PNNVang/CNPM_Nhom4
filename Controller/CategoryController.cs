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
    public async Task<IActionResult> getCategories()
    {
        var categories = from p in _context.Products
            join c in _context.Categories on p.CategoryId equals c.Id
            select new
            {
                categoryName=c.CategoryName
            };
        var categoriesList =  categories.ToList();
        List<CategoryNameDTO> categoryDtos = new List<CategoryNameDTO>();
        foreach (var c in categoriesList)
        {
            categoryDtos.Add(new CategoryNameDTO()
            {
                CategoryName = c.categoryName
            });
        }
        return Ok(categoryDtos);
    }

    [HttpGet("getcategoriesandproducts")]
    public async Task<IActionResult> getCategoriesAndProducts()
    {
        var categories = _context.Categories.Select(c => new CategoryNameDTO()
        {
            CategoryName = c.CategoryName,
            Products = c.Products.Select(product => new ProductDTO()
            {
                CategoryId = product.CategoryId,
            }).ToList()
        });
        return Ok(categories);
    }

}