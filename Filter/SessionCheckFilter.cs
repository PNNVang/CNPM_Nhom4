using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using Dot_Net_ECommerceWeb.Model;

namespace Dot_Net_ECommerceWeb.Service.Filter;

public class SessionCheckFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var userJson = session.GetString("user");

        // Nếu không tồn tại phiên người dùng, chuyển hướng đến trang đăng nhập
        if (string.IsNullOrEmpty(userJson))
        {
            context.Result = new RedirectToActionResult("login", "Account", null);
            return;
        }

        try
        {
            // Chuyển đổi dữ liệu người dùng từ JSON trong session
            var userData = JsonSerializer.Deserialize<User>(userJson);
            
            // Kiểm tra nếu không phải admin, chuyển hướng về trang đăng nhập
            if (userData?.Role?.ToLower() != "admin")
            {
                context.Result = new RedirectToActionResult("login", "Account", null);
                return;
            }
        }
        catch
        {
            // Nếu có lỗi xử lý dữ liệu session, chuyển hướng về trang đăng nhập
            context.Result = new RedirectToActionResult("login", "Account", null);
        }
    }
}

