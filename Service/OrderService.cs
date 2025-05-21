using System.Net;
using System.Net.Mail;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Service
{
    public class OrderService
    {
        private readonly AppDBContext _context;

        public OrderService(AppDBContext context)
        {
            _context = context;
        }

        // 7.1.8. OrderService gọi đến AppDBContext để thực hiện truy vấn xuống cơ sở dữ liệu để lấy dữ liệu
        public List<OrderSummaryViewModel> GetOrders()
        {
            return _context.Orders
                   .Include(o => o.user)
                   .Select(o => new OrderSummaryViewModel
                   {
                       Id = o.Id,
                       FullName = o.user.FullName,
                       CreatedAt = o.CreatedAt,
                       TotalPrice = o.TotalPrice,
                       Status = o.Status,
                       StatusPayment = o.StatusPayment
                   })
            .ToList();
        }
    }
}