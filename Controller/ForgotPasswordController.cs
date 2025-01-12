using System;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;


namespace Dot_Net_ECommerceWeb.Controllers
{
    [Route("Account")]
    public class ForgotPasswordController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(ForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        // GET: /ForgotPassword
        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View("~/Views/user/forgotPassword.cshtml");
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {

            if (await _forgotPasswordService.GetEmail(email))
            {
                string newPassword = _forgotPasswordService.CreateNewPassword();

                _forgotPasswordService.SendEmail(email, newPassword);

                // Cập nhật mật khẩu mới cho người dùng trong cơ sở dữ liệu
                bool result = await _forgotPasswordService.ResetPasswordAsync(email, newPassword);

                if (result)
                {
                    ViewData["Notify"] = "Mật khẩu mới đã được gửi qua email của bạn.";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewData["Notify"] = "Đã xảy ra lỗi khi cập nhật mật khẩu.";
                    return RedirectToAction("ForgotPassword", "Account");
                }
            }
            else
            {
                ViewData["Notify"] = "Email không tồn tại";
                return RedirectToAction("ForgotPassword", "Account");
            }
        }


    }
}