using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Service;

public class LogService
{
    private AppDBContext _context { get; set; }

    public LogService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Logs>> GetLogs()
    {
        return await _context.Logs.ToListAsync();
    }

}