using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Dot_Net_ECommerceWeb.Controller;
[Route("Account")]
public class LoginController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View("~/Views/User/Login.cshtml");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["Notify"] = "Vui lòng nhập đầy đủ thông tin";
                return RedirectToAction("Login", "Account");
            }

            string ipAddress = ServiceIPAddress.ConvertToIPv4(Request.HttpContext.Connection.RemoteIpAddress?.ToString());

            var user = await _loginService.LoginAsync(username, password, "web", "login", ipAddress);

            // Kiểm tra kết quả trả về từ LoginAsync
            if (user != null)
            {
                // Lưu thông tin người dùng vào Session
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));

                switch (user.Role)
                {
                    case "admin":
                        return RedirectToAction("admin_summary", "Admin");
                    case "user":
                        return RedirectToAction("Home", "Home");
                    case "prohibit":
                        HttpContext.Session.Remove("user");
                        return RedirectToAction("Prohibit", "Notification");
                    default:
                        return RedirectToAction("Login", "Account", new { notify = "Tài khoản không hợp lệ!" });
                }
            }

            ViewData["Notify"] = "Tài khoản hoặc mật khẩu không chính xác.";
            return View("~/Views/User/Login.cshtml");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình đăng nhập. Vui lòng thử lại sau.";
            return View("~/Views/Error/404.cshtml");
        }
    }
}
