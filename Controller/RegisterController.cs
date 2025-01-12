using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Dot_Net_ECommerceWeb.Model;

namespace Dot_Net_ECommerceWeb.Controllers
{
    [Route("Account")]
    public class RegisterController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly RegisterService registerService;

        public RegisterController(RegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("~/Views/User/Register.cshtml");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(string username, string password, string email)
        {
            if (!Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"))
            {
                ModelState.AddModelError("Email", "Email không hợp lệ!");
                return View("~/Views/User/Register.cshtml");
            }

            if (!CheckPassword(password))
            {
                ViewData["Notify"] = "Mật khẩu không hợp lệ! Phải có ít nhất 8 ký tự, 1 chữ hoa, 1 chữ số và không có khoảng trắng!";
                return View("~/Views/User/Register.cshtml");
            }

            var result = await registerService.CheckRegister(username, email);
            if (!result.isValid)
            {
                ViewData["Notification"] = result.errorMessage;
                return View("~/Views/User/Register.cshtml");
            }

            var insertResult = await registerService.InsertUserAsync(username, password, email);

            if (!insertResult)
            {
                ViewData["Notification"] = "Đăng ký không thành công, vui lòng thử lại.";
                return View("~/Views/User/Register.cshtml");
            }
            ViewData["Notification"] = "Đăng ký thành công, vui lòng đăng nhập.";
            return RedirectToAction("Login", "Account");
        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 8 || password.Length > 15) return false;
            if (password.Contains(" ")) return false;
            // Kiểm tra ký tự đặc biệt
            string specialCharactersPattern = "[^a-zA-Z0-9]";
            if (!Regex.IsMatch(password, specialCharactersPattern)) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsDigit)) return false;
            return true;
        }


    }
}
