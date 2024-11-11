namespace TodoList.Core.Models;

public class Todo
{
    public int Id { get; private set; }
    
    public string Description { get; private set; }
    public DateTime FinishDate { get; private set; }
    public bool IsDone { get; private set; }
    public int OwnerId { get; private set; }
    
}