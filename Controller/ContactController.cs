using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string FullName, string RecipientEmail, string Subject, string Message)
        {
            try
            {
                // Xác thực kết nối với Gmail SMTP
                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Cổng SMTP sử dụng StartTLS
                    Credentials = new NetworkCredential("xhoang345@gmail.com", "hrjn wuhx zcfb tqkc"), // Thông tin tài khoản Gmail của bạn
                    EnableSsl = true, // Kích hoạt SSL
                };

                // Tạo đối tượng email
                var mailMessage = new MailMessage
                {
                    // From = new MailAddress("xhoang345@gmail.com"), // Email của bạn
                    From = new MailAddress(RecipientEmail),
                    Subject = "Phản hồi", // Chủ đề lấy từ form
                    Body = "Cam on vi da phan hoi chung toi se tra loi lai trong thoi gian som nhat co the",
                    IsBodyHtml = false, // Đặt nội dung không phải HTML
                };

                mailMessage.To.Add(RecipientEmail); // Gửi tới email người dùng nhập vào

                // Gửi email
                smtpClient.Send(mailMessage);

                // Thông báo thành công
                ViewBag.Message = "Phản ánh của bạn đã được gửi thành công!";
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có
                ViewBag.Message = $"Có lỗi xảy ra khi gửi email. Chi tiết: {ex.Message}";
            }

            // Trả về lại View
            return View("Contact");
        }


    }
}
