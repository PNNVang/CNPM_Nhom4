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


        [HttpGet("getorders/all")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("getorders/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
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

        // Lấy chi tiết đơn hàng qua orderId cụ thể
        [HttpGet("getordersdetail/{id}")]
        public ActionResult<OrderDetailViewModel> GetOrderDetail(int id)
        {
            // Lấy thông tin chi tiết đơn hàng từ service
            var orderDetail = _orderService.GetOrderDetail(id);

            if (orderDetail == null)
            {
                return NotFound("Order not found");
            }

            return Ok(orderDetail);
        }

    }
}