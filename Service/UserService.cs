using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Service;

public class UserService
{
    private readonly AppDBContext _context;

    public UserService(AppDBContext context)
    {
        _context = context;
    }

    // Lấy danh sách người dùng
    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users.Select(user => new UserDTO
        {
            Id = user.Id,
            username = user.username ?? string.Empty,
            password = user.password ?? string.Empty,
            fullName = user.FullName ?? string.Empty,
            gender = user.Gender ?? string.Empty,
            birthday = user.Birthday,
            email = user.Email ?? string.Empty,
            phone = user.Phone ?? string.Empty,
            address = user.Address ?? string.Empty,
            avatar = user.Avatar ?? string.Empty,
            role = user.Role ?? string.Empty,
            status = user.Status ?? string.Empty,
            typeLogin = user.TypeLogin ?? string.Empty
        }).ToList();
    }

    // Xóa người dùng
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Status = "Đã xóa";
        await _context.SaveChangesAsync();
        return true;
    }

    // Cập nhật vai trò
    public async Task<bool> UpdateUserRoleAsync(int id, string newRole)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;

        user.Role = newRole;
        await _context.SaveChangesAsync();
        return true;
    }
}