using System.Net;
using System.Net.Mail;
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

        public bool SendMailOrder(User user, CustomerViewModel req, Order order)
        {
            try
            {
                // Xác thực kết nối với Gmail SMTP
                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Cổng SMTP sử dụng StartTLS
                    Credentials =
                        new NetworkCredential("xhoang345@gmail.com",
                            "hrjn wuhx zcfb tqkc"), // Thông tin tài khoản Gmail của bạn
                    EnableSsl = true, // Kích hoạt SSL
                };


                // Tạo đối tượng email
                var mailMessage = new MailMessage
                {
                    // From = new MailAddress("xhoang345@gmail.com"), // Email của bạn
                    From = new MailAddress(req.Email),
                    Subject = "Thông tin đặt hàng", // Chủ đề lấy từ form
                    Body = $"Chào {req.CustomerName}," +
                           $"Cảm ơn bạn đã mua hàng tại chúng tôi. Đơn hàng của bạn đã được xác nhận." +
                           $"Tổng tiền: {order.TotalPrice}" +
                           $"Hình thức thanh toán: {req.Payment}" +
                           $"Địa chỉ giao hàng: {req.Address}" +
                           "Cảm ơn bạn đã mua sắm cùng chúng tôi!",
                    IsBodyHtml = false, // Đặt nội dung không phải HTML
                };

                mailMessage.To.Add(req.Email); // Gửi tới email người dùng nhập vào

                // Gửi email
                smtpClient.Send(mailMessage);

                // Thông báo thành công
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}