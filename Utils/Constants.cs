using CloudinaryDotNet;

namespace Dot_Net_ECommerceWeb.Utils;

public class Constants
{
    public Cloudinary _cloudinary { get; set; }

    public Constants()
    {
        Account account = new Account(
            "your-cloud-name", // Cloud name
            "your-api-key", // API Key
            "your-api-secret" // API Secret
        );
        _cloudinary = new Cloudinary(account);
    }
}