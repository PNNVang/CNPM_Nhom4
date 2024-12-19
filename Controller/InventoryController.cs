using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("/api/[controller]")]
[ApiController]
public class InventoryController:Microsoft.AspNetCore.Mvc.Controller
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("getinventoriesdetail")]
    public async Task<IActionResult> GetInventory()
    {
        var inventories = await _inventoryService.GetInventoryDetailsAsync();
        return Ok(inventories);
    }
}