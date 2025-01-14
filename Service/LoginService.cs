using System;
using System.Threading.Tasks;
using Dot_Net_ECommerceWeb.Model;

namespace Dot_Net_ECommerceWeb.Service;

public class LoginService
{
    private readonly UserService _userService;
    private readonly EncryptAndDencrypt _encryptAndDencrypt;

    public LoginService(UserService userService, EncryptAndDencrypt encryptAndDencrypt)
    {
        _userService = userService;
        _encryptAndDencrypt = encryptAndDencrypt;
    }

    public async Task<User> LoginAsync(string username, string password, string typeLogin, string action, string ipAddress)
    {
        // Lấy người dùng từ cơ sở dữ liệu dựa trên tên người dùng
        var user = await _userService.GetUserByUsernameAsync(username);

        if (user == null)
        {
            return null;
        }
        // Kiểm tra nếu mật khẩu trống
        if (string.IsNullOrEmpty(password))
        {
            return null; // Trả về null nếu mật khẩu chưa được nhập
        }
        bool isPasswordValid = _encryptAndDencrypt.CheckPassword(password, user.password);

        if (isPasswordValid)
        {
            return user;
        }
        return null;

    }
}

