

using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Dot_Net_ECommerceWeb.Controller
{
    // [Route("api/[controller]")]
    // [ApiController]
    
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = users.Select(user => new UserDTO
            {
                username = user.username ?? string.Empty, // Sử dụng giá trị mặc định nếu NULL
                password = user.password ?? string.Empty, // Nếu cần thiết
                // fullName = user.FullName ?? string.Empty,
                // gender = user.Gender ?? string.Empty,
                // birthday = user.Birthday?.ToString("yyyy-MM-dd") ?? string.Empty, // Chuyển đổi DateTime sang chuỗi
                // email = user.Email ?? string.Empty,
                // phone = user.Phone ?? string.Empty,
                // address = user.Address ?? string.Empty,
                // avatar = user.Avatar ?? string.Empty,
                // role = user.Role ?? string.Empty,
                // status = user.Status ?? string.Empty,
                // typeLogin = user.TypeLogin ?? string.Empty
            }).ToList();

            return Ok(userDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<users>> PostUser(users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, users user)
        {
            if (id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
