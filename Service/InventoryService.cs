using Dot_Net_ECommerceWeb.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Service;

public class InventoryService
{
    private readonly AppDBContext _context;

    public InventoryService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<object>> GetInventoryDetailsAsync()
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

        return inventories.Cast<object>().ToList();
    }
}