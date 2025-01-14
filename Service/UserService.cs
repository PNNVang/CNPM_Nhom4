using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
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
        }).Where(u => u.status == "chưa xóa").ToList();
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

    //Thêm người dùng
    public async Task<bool> AddUserAsync(User user, string action, string ipAddress)
    {
        try
        {
            await _context.Users.AddAsync(user);

            var log = new Logs
            {
                ip_address = ipAddress,
                current_value = action,
                created_at = DateTime.UtcNow
            };
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return false;
        }
    }

    //Cập nhật thông tin người dùng
    public async Task<bool> UpdateUserAsync(User user, string action, string ipAddrress)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null) return false;

        existingUser.username = user.username;
        existingUser.password = user.password;
        existingUser.FullName = user.FullName;
        existingUser.Gender = user.Gender;
        existingUser.Birthday = user.Birthday;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;
        existingUser.Address = user.Address;
        existingUser.Avatar = user.Avatar;
        existingUser.Role = user.Role;
        existingUser.Status = user.Status;
        existingUser.TypeLogin = user.TypeLogin;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var log = new Logs
        {
            ip_address = ipAddrress,
            current_value = action,
            created_at = DateTime.UtcNow
        };
        await _context.Logs.AddAsync(log);
        await _context.SaveChangesAsync();

        return true;
    }

    //Tìm 
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.username == username);
    }
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    //Kiểm tra xem đã tồn tại username, email chưa
    public async Task<bool> CheckDuplicatedEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> CheckDuplicatedUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.username.ToLower() == username.ToLower());
    }


}