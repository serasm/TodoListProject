using Microsoft.AspNetCore.Identity;

namespace TodoList.Core.Models;

public class User : IdentityUser<int>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}