using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;

    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _context;

        public OrdersController(AppDBContext context)
        {
            _context = context;
        }

        // api lay thong tin don hang dang cho giao
        [HttpGet("getorder_waiting")]
        public async  Task<IActionResult> GetWaitingOrders()
        {
            var orders =  _context.Orders.Where(o => o.Status == "chờ xác nhận").ToList();
            return Ok(orders);
        }

        // api lay thong tin don hang dang giao
        [HttpGet("getorder_giving")]
        public async Task<IActionResult> GetGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đang giao").ToList();
            return Ok(orders);
        }

        // api lay thong tin don hang da giao
        [HttpGet("getorder_gived")]
        public async Task<IActionResult> GetGivedOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đã giao").ToList();
            return Ok(orders);
        }

        // api lay thong tin don hang da bi huy
        [HttpGet("getorder_cancelled")]
        public async Task<IActionResult> GetCanceledOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "hủy").ToList();
            return Ok(orders);
        }

        // api lay thong tin dat hang dang cho giao
        [HttpGet("getorder_waitinggiving")]
        public async Task<IActionResult> GetWaitingGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đang chờ giao").ToList();
            return Ok(orders);
        }

        //api cap nhat trang thai don hang
        [HttpPut("updateorder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id,[FromQuery] string status)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound("Not found order by requirement");
            }
            order.Status = status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
        
    }

