using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Dot_Net_ECommerceWeb.Service.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    [SessionCheckFilter]
    public class UserAdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserService _userService;

        public UserAdminController(UserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp email và username
                if (await _userService.CheckDuplicatedEmailAsync(user.Email))
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống.");
                    return View(user);
                }

                if (await _userService.CheckDuplicatedUsernameAsync(user.username))
                {
                    ModelState.AddModelError("username", "Tên đăng nhập đã tồn tại trong hệ thống.");
                    return View(user);
                }

                user.Status = "chưa xóa";
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var action = $"Thêm người dùng: {user.username}";

                var result = await _userService.AddUserAsync(user, action, ipAddress);

                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm người dùng.");
                }
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Convert DTO to Model for editing
            var userModel = new User
            {
                Id = user.Id,
                username = user.username,
                password = user.password,
                FullName = user.fullName,
                Gender = user.gender,
                Birthday = user.birthday,
                Email = user.email,
                Phone = user.phone,
                Address = user.address,
                Avatar = user.avatar,
                Role = user.role,
                Status = user.status,
                TypeLogin = user.typeLogin
            };

            return View(userModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp email (trừ user hiện tại)
                var existingUserByEmail = await _userService.GetUserByEmailAsync(user.Email);
                if (existingUserByEmail != null && existingUserByEmail.Id != user.Id)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống.");
                    return View(user);
                }

                // Kiểm tra trùng lặp username (trừ user hiện tại)
                var existingUserByUsername = await _userService.GetUserByUsernameAsync(user.username);
                if (existingUserByUsername != null && existingUserByUsername.Id != user.Id)
                {
                    ModelState.AddModelError("username", "Tên đăng nhập đã tồn tại trong hệ thống.");
                    return View(user);
                }

                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var action = $"Cập nhật thông tin người dùng: {user.username}";

                var result = await _userService.UpdateUserAsync(user, action, ipAddress);

                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy người dùng hoặc có lỗi xảy ra.");
                }
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Xóa người dùng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng hoặc có lỗi xảy ra.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: User/UpdateRole
        [HttpPost]
        public async Task<IActionResult> UpdateRole(int id, string newRole)
        {
            var result = await _userService.UpdateUserRoleAsync(id, newRole);

            if (result)
            {
                return Json(new { success = true, message = "Cập nhật vai trò thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Không tìm thấy người dùng hoặc có lỗi xảy ra." });
            }
        }

        // GET: User/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            var users = await _userService.GetUsersAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u =>
                    u.username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.fullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View("Index", users);
        }
    }
}
