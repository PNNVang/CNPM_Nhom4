using System.Security.Claims;
using System.Text.Json;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller
{
    [Route("/login")]
    [ApiController]
    public class LoginGoogleController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _context;

        public LoginGoogleController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("google-login")]
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Principal == null)
            {
                return Unauthorized();
            }
            
            var user = new User
            {
                username = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                Email = result.Principal.FindFirstValue(ClaimTypes.Email),
                FullName = result.Principal.FindFirstValue(ClaimTypes.Name),
                Avatar = result.Principal.FindFirstValue("picture"),
                TypeLogin = "google",
                Status = "chưa xóa",
                // Role = "user",
                Role="admin",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                password = "chưa xác định"
            };
            // Các thuộc tính khác sẽ tự động là null/default value

            // Kiểm tra email đã tồn tại
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null)
            {
                _context.Users.Add(user);
            }
            else
            {
                existingUser.FullName = user.FullName;
                existingUser.Avatar = user.Avatar;
                existingUser.UpdatedAt = DateTime.Now;
                _context.Users.Update(existingUser);
            }

            await _context.SaveChangesAsync();
            string userJson = JsonSerializer.Serialize(user);
            HttpContext.Session.SetString("user",userJson);
           return View("~/Views/Home/Index.cshtml");
        }
    }
}