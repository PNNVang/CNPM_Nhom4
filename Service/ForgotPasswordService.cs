using System.Net.Mail;
using System.Net;

namespace Dot_Net_ECommerceWeb.Service;

public class ForgotPasswordService
{
    private readonly UserService _userService;
    private readonly EncryptAndDencrypt _encryptAndDencrypt;
    public ForgotPasswordService(UserService userService, EncryptAndDencrypt encryptAndDencrypt)
    {
        _userService = userService;
        _encryptAndDencrypt = encryptAndDencrypt;
    }

    public async Task<bool> GetEmail(string email)
    {
        var user = await _userService.GetUsersAsync();
        return user.Any(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public void SendEmail(string receiveEmail, string newPassword)
    {
        try
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Sử dụng SSL
                Credentials = new NetworkCredential("xhoang345@gmail.com", "hrjn wuhx zcfb tqkc"), // Thông tin tài khoản Gmail của bạn
                EnableSsl = true,
            };

            // Tạo đối tượng email
            var mailMessage = new MailMessage
            {
                From = new MailAddress("xhoang345@gmail.com"),
                Subject = "Đổi mật khẩu",
                Body = "Mật khẩu mới của bạn là: " + newPassword,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(receiveEmail);

            // Gửi email
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            // Xử lý lỗi gửi email nếu có
            throw new Exception("Lỗi khi gửi email: " + ex.Message);
        }
    }

    public string CreateNewPassword()
    {
        string characters = "qwertyuiopasdfghjklzxcvbnm0123456789!@*";
        var random = new Random();
        string result = string.Empty;
        for (int i = 0; i < 8; i++)
        {
            int index = random.Next(characters.Length);
            result += characters[index];
        }
        return result;
    }

    // Cập nhật mật khẩu mới cho người dùng
    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        user.password = _encryptAndDencrypt.HashPassword(newPassword);

        return await _userService.UpdateUserAsync(user, "reset password", "unknown IP");
    }
}