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

        // GET: api/orders/getorder_waiting
        [HttpGet("getorder_waiting")]
        public async  Task<IActionResult> GetWaitingOrders()
        {
            var orders =  _context.Orders.Where(o => o.Status == "chờ xác nhận").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_giving
        [HttpGet("getorder_giving")]
        public async Task<IActionResult> GetGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đang giao").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_gived
        [HttpGet("getorder_gived")]
        public async Task<IActionResult> GetGivedOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đã giao").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_canceled
        [HttpGet("getorder_cancelled")]
        public async Task<IActionResult> GetCanceledOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "hủy").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_waitinggiving
        [HttpGet("getorder_waitinggiving")]
        public async Task<IActionResult> GetWaitingGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "đang chờ giao").ToList();
            return Ok(orders);
        }

        // POST: api/orders/updateorder
        [HttpPost("updateorder")]
        public async Task<IActionResult> UpdateOrder([FromForm] int id, [FromForm] string select)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = select;
            _context.SaveChanges();

            return Ok(new { message = "Cập nhật thành công!" });
        }
    }

