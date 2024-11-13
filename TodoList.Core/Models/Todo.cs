namespace TodoList.Core.Models;

public class Todo
{
    public int Id { get; private set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsDone { get; set; }
    public int OwnerId { get; set; }
    public virtual User Owner { get; set; }
    
}