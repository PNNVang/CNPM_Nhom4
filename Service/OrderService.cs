using System.Net;
using System.Net.Mail;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;

namespace Dot_Net_ECommerceWeb.Service
{
    public class OrderService
    {
        private readonly AppDBContext _context;

        public OrderService(AppDBContext context)
        {
            _context = context;
        }

        // Phương thức lấy ra danh sách đơn hàng
        public List<OrderSummaryViewModel> GetOrders()
        {
            return _context.Orders
                   .Include(o => o.user) // để truy cập được FullName của user
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

        // Phương thức lấy ra danh sách đơn hàng theo trạng thái đơn hàng
        public List<OrderSummaryViewModel> GetOrdersByStatus(string status)
        {
            string translatedStatus = status switch
            {
                "waiting" => "Chờ xác nhận",
                "giving" => "Đang giao hàng",
                "gived" => "Đã giao",
                "cancelled" => "Đã hủy",
                "waiting_giving" => "Chờ giao hàng",
                _ => null
            };

            return _context.Orders
                .Where(o => o.Status == translatedStatus)
                .Include(o => o.user) // để truy cập được FullName của user
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


        public async Task<Order> UpdateOrderStatusAsync(int id, string newStatus)
        {
            string translatedStatus = newStatus switch
            {
                "waiting" => "Chờ xác nhận",
                "giving" => "Đang giao hàng",
                "gived" => "Đã giao",
                "cancelled" => "Đã hủy",
                "waiting_giving" => "Chờ giao hàng",
                _ => null
            };
            var order = _context.Orders.Find(id);
            if (order == null) return null;

            order.Status = translatedStatus;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Lấy chi tiết đơn hàng qua orderId cụ thể
        public OrderDetailViewModel GetOrderDetail(int orderId)
        {
            // Truy vấn dữ liệu từ bảng OrderDetails trong CSDL, lọc ra các chi tiết đơn hàng với OrderId tương ứng
            var orderDetails = _context.OrderDetails
                                       .Where(od => od.OrderId == orderId) // Lọc theo OrderId
                                       .Select(od => new
                                       {
                                           Order = od.Order, // Lấy thông tin của đơn hàng (Order)
                                           User = od.Order.user, // Lấy thông tin người dùng từ đơn hàng (User)
                                           Product = od.Product, // Lấy thông tin sản phẩm (Product)
                                           ProductImage = _context.ProductImages.FirstOrDefault(pi => pi.Id == od.Product.Id), // Lấy hình ảnh sản phẩm (ProductImage) từ bảng ProductImages, dựa trên ProductId
                                           QuantityTotal = od.QuantityTotal // Lấy số lượng tổng của sản phẩm trong đơn hàng (QuantityTotal)
                                       }).ToList(); // Chuyển kết quả thành một danh sách (List)

            // Lấy chi tiết của đơn hàng đầu tiên trong orderDetails
            var firstDetail = orderDetails.First();

            // Khởi tạo một đối tượng OrderDetailViewModel để trả về dữ liệu chi tiết đơn hàng
            var viewModel = new OrderDetailViewModel
            {
                OrderId = orderId,
                CreatedAt = firstDetail.Order.CreatedAt,
                OrderStatus = firstDetail.Order.Status,
                Note = firstDetail.Order.Note,
                ShippingCost = decimal.TryParse(firstDetail.Order.ShipCost, out var shipCost) ? shipCost : 0,
                UserId = firstDetail.Order.UserId ?? 0,
                UserFullName = firstDetail.User?.FullName,

                Products = orderDetails.Select(o => new OrderDetailViewModel.ProductDetail
                {
                    ProductImage = o.ProductImage?.ImgMain,
                    ProductName = o.Product.ProductName,
                    Quantity = o.QuantityTotal,
                    Price = (decimal)o.Product.Price
                }).ToList()
            };

            // Tính tổng giá trị của tất cả sản phẩm trong đơn hàng (giá * số lượng)
            viewModel.TotalPrice = viewModel.Products.Sum(p => p.Price * (int)p.Quantity);

            // Tính tổng số tiền cần thanh toán, bao gồm cả chi phí vận chuyển
            viewModel.TotalAmount = viewModel.TotalPrice + viewModel.ShippingCost;

            return viewModel;
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