using Dot_Net_ECommerceWeb.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OrdersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/orders/getorder_waiting
        [HttpGet("getorder_waiting")]
        public IActionResult GetWaitingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "Chờ xác nhận").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_giving
        [HttpGet("getorder_giving")]
        public IActionResult GetGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "Đang giao").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_gived
        [HttpGet("getorder_gived")]
        public IActionResult GetGivedOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "Đã giao").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_canceled
        [HttpGet("getorder_canceled")]
        public IActionResult GetCanceledOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "Hủy").ToList();
            return Ok(orders);
        }

        // GET: api/orders/getorder_waitinggiving
        [HttpGet("getorder_waitinggiving")]
        public IActionResult GetWaitingGivingOrders()
        {
            var orders = _context.Orders.Where(o => o.Status == "Đang chờ giao").ToList();
            return Ok(orders);
        }

        // POST: api/orders/updateorder
        [HttpPost("updateorder")]
        public IActionResult UpdateOrder([FromForm] int id, [FromForm] string select)
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

