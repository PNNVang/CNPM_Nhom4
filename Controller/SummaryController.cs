using System.Text.RegularExpressions;
using Dot_Net_ECommerceWeb.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class SummaryController:Microsoft.AspNetCore.Mvc.Controller
{
    private AppDBContext _context;

    public SummaryController(AppDBContext context)
    {
        _context = context;
    }
//api lay tong doanh thu
    [HttpGet("getsummary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await (from order in _context.Orders
            join order_detail in _context.OrderDetails
                on order.Id equals order_detail.OrderId
                group order by order.CreatedAt.Month into g
            select new
            {
                Month = g.Key,
                Sum=g.Sum(x=>x.TotalPrice)
            }).ToListAsync();
        return Ok(summary);
    }
}