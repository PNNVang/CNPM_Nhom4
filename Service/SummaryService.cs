using Dot_Net_ECommerceWeb.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Service;

public class SummaryService
{
    private readonly AppDBContext _context;

    public SummaryService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<object>> GetMonthlyRevenueSummaryAsync()
    {
        var summary = await (from order in _context.Orders
            join order_detail in _context.OrderDetails
                on order.Id equals order_detail.OrderId
            group order by order.CreatedAt.Month
            into g
            select new
            {
                Month = g.Key,
                Sum = g.Sum(x => x.TotalPrice)
            }).ToListAsync();

        return summary.Cast<object>().ToList();
    }
}