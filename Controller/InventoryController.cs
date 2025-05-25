using Dot_Net_ECommerceWeb.DBContext; 
using Dot_Net_ECommerceWeb.Model; 
using Dot_Net_ECommerceWeb.Service; 
using Microsoft.AspNetCore.Http.HttpResults; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller
{
    // Định nghĩa route cho controller: /api/inventory
    [Route("/api/[controller]")]
    [ApiController]
    public class InventoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly InventoryService _inventoryService;

        // Constructor nhận vào dịch vụ quản lý tồn kho thông qua Dependency Injection
        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // API lấy thông tin số lượng sản phẩm trong kho
        [HttpGet("getinventoriesdetail")]
        public async Task<IActionResult> GetInventory()
        {
            // Gọi service để lấy thông tin tồn kho
            var inventories = await _inventoryService.GetInventoryDetailsAsync();
            
            // Trả về dữ liệu dưới dạng HTTP 200 OK với dữ liệu tồn kho
            return Ok(inventories);
        }
    }
}
