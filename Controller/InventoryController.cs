using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class InventoryController:Microsoft.AspNetCore.Mvc.Controller
{
    private AppDBContext _context { get; set; }

    public InventoryController(AppDBContext context)
    {
        _context = context;
    }
    [HttpGet("getinventoriesdetail")]
    public  async Task<IActionResult> GetInventory()
    {
        var inventories = await (from i in _context.InventoryDetails
            join p in _context.Products on i.ProductId equals p.Id
            group i by i.ProductId into grouped
            select new 
            {
                ProductId = grouped.Key,
                TotalQuantity = grouped.Sum(x => x.Quantity),
                TotalPrice = grouped.Sum(x => x.Quantity * x.Price)
            }).ToListAsync();
        return Ok(inventories);
        }
}