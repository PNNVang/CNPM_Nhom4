using System.Security.Claims;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;
[Microsoft.AspNetCore.Components.Route("/api/[controller]")]
[ApiController]
public class TestController:Microsoft.AspNetCore.Mvc.Controller
{
    private PasswordHasher<TestUser> _passwordHasher;
   private TestUser _user;
    public TestController(PasswordHasher<TestUser> passwordHasher, TestUser user)
    {
        _passwordHasher = passwordHasher;
        _user = user;
    }

    public string HashPassword(string password)
    {
        var hashedPassword = _passwordHasher.HashPassword(_user, password);
        return hashedPassword;
    }
    public async Task<IActionResult> GoogleLogin()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var externalUser = result.Principal;

        var name = externalUser?.FindFirst(ClaimTypes.Name)?.Value;
        var email = externalUser?.FindFirst(ClaimTypes.Email)?.Value;
        // Lưu thông tin người dùng hoặc làm gì đó sau khi đăng nhập
        return RedirectToAction("Index", "Home");
    }
}