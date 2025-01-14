using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dot_Net_ECommerceWeb.Model;
[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    
    public int? Id { get; set; }
    [Column("username")]
    public string? username { get; set; }
    [Column("password")]
    public string password { get; set; }
    [Column("full_name")]
    public string? FullName { get; set; }
    [Column("gender")]
    public string? Gender { get; set; }
    [Column("birthday")]
    public DateTime? Birthday { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("phone")]
    public string? Phone { get; set; }
    [Column("address")]
    public string? Address { get; set; }
    [Column("avatar")]
    public string? Avatar { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } 
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("delete_at")]
    public DateTime? DeletedAt { get; set; }
    [Column("role")]
    public string? Role { get; set; }
    [Column("status")]
    public string? Status { get; set; }
    [Column("type_login")]
    public string? TypeLogin { get; set; }
    public List<Order> Orders { get; set; }

    // public User(string username, string password)
    // {
    //     this.username = username;
    //     this.password = password;
    // }
}