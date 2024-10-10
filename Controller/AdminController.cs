using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;

public class AdminController:Microsoft.AspNetCore.Mvc.Controller
{
    public IActionResult admin_user()
    {
        return View();
    }

    public IActionResult admin_image()
    {
        return View();
    }
}