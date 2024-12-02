using System.Text.RegularExpressions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class SummaryController:Microsoft.AspNetCore.Mvc.Controller
{
    private readonly SummaryService _summaryService;

    public SummaryController(SummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("getsummary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _summaryService.GetMonthlyRevenueSummaryAsync();
        return Ok(summary);
    }
}