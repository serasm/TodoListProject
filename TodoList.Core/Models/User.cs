using Microsoft.AspNetCore.Identity;

namespace TodoList.Core.Models;

public class User : IdentityUser<int>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<Todo> Todos { get; set; }
}