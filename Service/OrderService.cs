using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;

namespace Dot_Net_ECommerceWeb.Service
{
    public class OrderService
    {
        private readonly AppDBContext _context;

        public OrderService(AppDBContext context)
        {
            _context = context;
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            string translatedStatus = status switch
            {
                "waiting" => "chờ xác nhận",
                "giving" => "đang giao",
                "gived" => "đã giao",
                "cancelled" => "hủy",
                "waiting_giving" => "đang chờ giao",
                _ => null
            };
            return _context.Orders.Where(o => o.Status == translatedStatus).ToList();
        }

        public async Task<Order> UpdateOrderStatusAsync(int id, string newStatus)
        {
            string translatedStatus = newStatus switch
            {
                "waiting" => "chờ xác nhận",
                "giving" => "đang giao",
                "gived" => "đã giao",
                "cancelled" => "hủy",
                "waiting_giving" => "đang chờ giao",
                _ => null
            };
            var order = _context.Orders.Find(id);
            if (order == null) return null;

            order.Status = translatedStatus;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}