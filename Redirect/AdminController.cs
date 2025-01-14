using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Dot_Net_ECommerceWeb.Controller;
[SessionCheckFilter]
public class AdminController : Microsoft.AspNetCore.Mvc.Controller
{
    public IActionResult admin()
    {
        return View();
    }

    public IActionResult admin_backward_product()
    {
        return View();
    }

    public IActionResult admin_form_update(int id)
    {
        HttpContext.Session.SetString("id", id.ToString());
        return View();
    }

    public IActionResult admin_form_upload_product()
    {
        return View();
    }

    public IActionResult admin_header()
    {
        return View();
    }

    public IActionResult admin_image()
    {
        return View();
    }

    public IActionResult admin_inventory()
    {
        return View();
    }

    public IActionResult admin_log()
    {
        return View();
    }

    public IActionResult admin_order()
    {
        return View();
    }

    public IActionResult admin_product()
    {
        return View();
    }

    public IActionResult admin_summary()
    {
        return View();
    }

    public IActionResult admin_user()
    {
        return View();
    }

    public IActionResult admin_editor()
    {
        return View();
    }

    public IActionResult admin_categories()
    {
        return View();
    }

    public IActionResult admin_login_google()
    {
        return View();
    }

    public IActionResult admin_paypal()
    {
        return View();
    }

    public IActionResult admin_logout()
    {
        return RedirectToAction("Login","Account");
    }
}