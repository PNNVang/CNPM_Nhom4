using Dot_Net_ECommerceWeb.Service;

public class ChangePasswordService
{
    private readonly UserService _userService;
    private readonly EncryptAndDencrypt _encryptAndDecrypt;

    public ChangePasswordService(UserService userService, EncryptAndDencrypt encryptAndDencrypt)
    {
        _userService = userService;
        _encryptAndDecrypt = encryptAndDencrypt;
    }


    public async Task<bool> UpdatePasswordAsync(string username, string newPassword, string action, string ipAddress)
    {
        try
        {
            // Lấy user dựa vào username
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return false; // Không tìm thấy người dùng
            }

            // Mã hóa mật khẩu mới
            user.password = _encryptAndDecrypt.HashPassword(newPassword);

            // Cập nhật thông tin user
            if (await _userService.UpdateUserAsync(user, action, ipAddress))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating password: {ex.Message}");
            return false;
        }
    }
}