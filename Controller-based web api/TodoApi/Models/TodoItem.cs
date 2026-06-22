namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Nam { get; set; }
    public bool IsComplete { get; set; }
}