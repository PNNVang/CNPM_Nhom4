

using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Dot_Net_ECommerceWeb.Controller
{
    [Route("/api/[controller]")]
    [ApiController]
    
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }
       //api lay thong tin nguoi dung
        [HttpGet("getuser")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = users.Select(user => new UserDTO
            {
                Id = user.Id,
                username = user.username ?? string.Empty, // Sử dụng giá trị mặc định nếu NULL
                password = user.password ?? string.Empty, // Nếu cần thiết
                fullName = user.FullName ?? string.Empty,
                gender = user.Gender ?? string.Empty,
                birthday = user.Birthday, // Chuyển đổi DateTime sang chuỗi
                email = user.Email ?? string.Empty,
                phone = user.Phone ?? string.Empty,
                address = user.Address ?? string.Empty,
                avatar = user.Avatar ?? string.Empty,
                role = user.Role ?? string.Empty,
                status = user.Status ?? string.Empty,
                typeLogin = user.TypeLogin ?? string.Empty
            }).ToList();

            return Ok(userDtos);
        }
        
    
//api xoa nguoi dung
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

          
            user.Status = "Đã xóa";
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //api cap nhat quyen cua user
        [HttpPut("updaterole/{id}")]
        public async Task<IActionResult> UpdateRole(int id,[FromBody] string role)
        {
            var user= await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            if (user != null)
            {
               user.Role = role;
               await _context.SaveChangesAsync();
               return Ok("Role updated");
            }
            return StatusCode(500,"Not Execute");
        }
    }
   
}
