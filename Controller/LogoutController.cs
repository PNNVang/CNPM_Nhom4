using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;

public class LogoutController:Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("user");
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}