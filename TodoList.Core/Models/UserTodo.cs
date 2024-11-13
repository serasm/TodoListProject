namespace TodoList.Core.Models;

public class UserTodo
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
}