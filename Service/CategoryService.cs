using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Service;

public class CategoryService
{
    private AppDBContext _context { get; set; }

    public CategoryService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var categories=_context.Categories.ToList();
        return await Task.FromResult(categories);
    }
}