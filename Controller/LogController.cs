using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class LogController:Microsoft.AspNetCore.Mvc.Controller
{
    private readonly LogService _logService;

    public LogController(LogService logService)
    {
        _logService = logService;
    }
//api lay log lich su
    [HttpGet("getloglist")]
    public async Task<IActionResult> getListLog()
    {
        var logList = await _logService.GetLogs(); // Thêm await
        return Ok(logList);
    }
}