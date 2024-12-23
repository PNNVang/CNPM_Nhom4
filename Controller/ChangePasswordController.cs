// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
// using Dot_Net_ECommerceWeb.Service;
//
// namespace Dot_Net_ECommerceWeb.Controllers
// {
//     [Route("Account")]
//     public class ChangePasswordController : Microsoft.AspNetCore.Mvc.Controller
//     {
//         private readonly ChangePasswordService _changePasswordService;
//
//         public ChangePasswordController(ChangePasswordService changePasswordService)
//         {
//             _changePasswordService = changePasswordService;
//         }
//
//         // GET: /changepassword
//         [HttpGet("ChangePassword")]
//         public IActionResult ChangePassword()
//         {
//             return View("~/Views/User/Changepassword.cshtml");
//         }
//
//         // POST: /changepassword
//         [HttpPost("ChangePassword")]
//         public async Task<IActionResult> ChangePassword(string currentpassword, string newpassword, string repeatpassword)
//         {
//             var username = HttpContext.Session.GetString("username");
//             var ipAddress = ServiceIPAddress.ConvertToIPv4(HttpContext.Connection.RemoteIpAddress?.ToString());
//
//             if (string.IsNullOrEmpty(username))
//             {
//                 return RedirectToAction("Login", "Account");
//             }
//
//             if (currentpassword == newpassword)
//             {
//                 ViewData["Notify"] = "Mật khẩu mới phải khác với mật khẩu hiện tại.";
//                 return View("~/Views/User/Changepassword.cshtml");
//             }
//
//             if (newpassword != repeatpassword)
//             {
//                 ViewData["Notify"] = "Mật khẩu mới không trùng khớp.";
//                 return View("~/Views/User/Changepassword.cshtml");
//             }
//             try
//             {
//                 bool result = await _changePasswordService.UpdatePasswordAsync(username, newpassword, "change password", ipAddress);
//                 if (result)
//                 {
//                     ViewData["Notify"] = "Đổi mật khẩu thành công.";
//                     return RedirectToAction("Login", "Account");
//                 }
//                 else
//                 {
//                     ViewData["Notify"] = "Đổi mật khẩu thất bại. Vui lòng thử lại.";
//                     return View("~/Views/User/Changepassword.cshtml");
//                 }
//             }
//             catch (Exception ex)
//             {
//                 ViewData["Notify"] = "Đã xảy ra lỗi trong quá trình đổi mật khẩu.";
//                 return View("~/Views/User/Changepassword.cshtml");
//             }
//         }
//     }
// }
