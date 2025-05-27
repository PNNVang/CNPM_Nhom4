using Dot_Net_ECommerceWeb.DBContext; 
using Dot_Net_ECommerceWeb.Model; 
using Dot_Net_ECommerceWeb.Service; 
using Microsoft.AspNetCore.Http.HttpResults; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller
{
    // 8.6: Giao tiếp qua API /api/inventory
    [Route("/api/[controller]")]
    [ApiController]
    public class InventoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly InventoryService _inventoryService;

        // 8.7: Inject service quản lý tồn kho
        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // 8.7: API lấy dữ liệu tồn kho sản phẩm
        [HttpGet("getinventoriesdetail")]
        public async Task<IActionResult> GetInventory()
        {
            // 8.7: Gọi hàm trong service để lấy danh sách tồn kho
            var inventories = await _inventoryService.GetInventoryDetailsAsync();
            
            // 8.8: Trả danh sách tồn kho dạng JSON
            return Ok(inventories);
        }
    }
}
