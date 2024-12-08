using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    [Route("/login")]
    [ApiController]
    public class LoginGoogleController : Microsoft.AspNetCore.Mvc.Controller
    {
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

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return new JsonResult(claims);
        }
    }
}