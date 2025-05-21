using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // 7.1.7. OrderController gọi đến phương thức GetOrders() trong OrderService
        [HttpGet("getorders/all")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }

    }
}