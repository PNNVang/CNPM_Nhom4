using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // API lấy thông tin người dùng
        [HttpGet("getuser")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        // API xóa người dùng
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        // API cập nhật quyền của người dùng
        [HttpPut("updaterole/{id}/{selected_value}")]
        public async Task<IActionResult> UpdateRole(int id, string selected_value)
        {
            var result = await _userService.UpdateUserRoleAsync(id, selected_value);
            if (!result) return NotFound(new { message = "User not found" });

            return Ok(new { message = "Role updated successfully", userId = id, newRole = selected_value });
        }
    }
}