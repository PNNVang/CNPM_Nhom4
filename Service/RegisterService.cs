﻿using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using System.Text;
using System.Threading.Tasks;
using Dot_Net_ECommerceWeb.DBContext;

public class RegisterService
{
    private readonly UserService userService;
    private readonly EncryptAndDencrypt encryptAndDencrypt;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDBContext _context;
    public RegisterService(UserService userService, EncryptAndDencrypt encryptAndDencrypt, IHttpContextAccessor httpContextAccessor, AppDBContext _context)
    {
        this.userService = userService;
        this.encryptAndDencrypt = encryptAndDencrypt;
        _httpContextAccessor = httpContextAccessor;
        this._context = _context;
    }

    public async Task<bool> InsertUserAsync(string username, string password, string email)
    {
        string hashedPassword = encryptAndDencrypt.HashPassword(password);

        // Thêm người dùng
        var newUser = new User
        {
            username = username,
            password = hashedPassword,
            Email = email,
            Role = "user",
            TypeLogin = "web",
            CreatedAt = DateTime.Now,
            Status = "chưa xóa"
        };

        return await userService.AddUserAsync(newUser, "register account", _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown");

    }

    public async Task<(bool isValid, string errorMessage)> CheckRegister(string username, string email)
    {
        string errorMessage = string.Empty;

        // Sử dụng await để chờ kết quả của các phương thức bất đồng bộ
        if (await userService.CheckDuplicatedUsernameAsync(username))
            errorMessage += "Tài khoản đã tồn tại.";

        if (await userService.CheckDuplicatedEmailAsync(email))
            errorMessage += " Email đã tồn tại.";

        return (string.IsNullOrEmpty(errorMessage), errorMessage);
    }

}
