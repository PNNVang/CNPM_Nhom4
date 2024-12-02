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

        [HttpGet("getorders/{status}")]
        public async Task<IActionResult> GetOrders(string status)
        {
            var orders =  _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpPut("updateorder/{id}/{status}")]
        public async Task<IActionResult> UpdateOrder(int id, string status)
        {
            var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, status);
            if (updatedOrder == null) return NotFound("Order not found");

            return Ok(updatedOrder);
        }
    }
}