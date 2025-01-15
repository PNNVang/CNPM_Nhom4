using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Identity;

public class EncryptAndDencrypt
{
	private readonly PasswordHasher<User> _passwordHasher;

	public EncryptAndDencrypt()
	{
		_passwordHasher = new PasswordHasher<User>();
	}

	// Mã hóa mật khẩu
	public string HashPassword(string password)
	{
		return _passwordHasher.HashPassword(new User(), password);
	}

	// Kiểm tra mật khẩu
	public bool CheckPassword(string plainPassword, string hashedPassword)
	{
		var result = _passwordHasher.VerifyHashedPassword(new User(), hashedPassword, plainPassword);

		return result == PasswordVerificationResult.Success;
	}
}

