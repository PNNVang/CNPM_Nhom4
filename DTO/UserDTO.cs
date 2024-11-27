using Newtonsoft.Json;

namespace Dot_Net_ECommerceWeb.DTO;

public class UserDTO
{
    public int? Id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string fullName { get; set; }
    public string gender { get; set; }
    public DateTime? birthday { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string address { get; set; }
    public string avatar { get; set; }
    [JsonIgnore]
    public DateTime createdAt { get; set; } = DateTime.Now;
    public DateTime? updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
    public string role { get; set; }
    public string status { get; set; }
    public string typeLogin { get; set; }
}