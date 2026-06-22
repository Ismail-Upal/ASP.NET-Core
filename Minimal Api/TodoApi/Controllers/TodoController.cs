using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace TodoApi.Controllers;

[ApiController]
[Route("api/")]
public class TodoController : ControllerBase
{
    private readonly TodoDb _db;

    public TodoController(TodoDb db)
    {
        _db = db;
    }


    [HttpGet("")]
    public string Main()
    {
        return "Hello world!";
    }


    [HttpGet("todoitems")]
    public async Task<ActionResult<List<Todo>>> GetAllTodos()
    {
        return await _db.Todos.ToListAsync();
    }


    [HttpGet("todoitems/complete")]
    public async Task<ActionResult<List<Todo>>> GetCompleteTodos()
    {
        return await _db.Todos.Where(t => t.IsComplete).ToListAsync();
    }


    [HttpGet("todoitems/{id}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    {
        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return NotFound(); // Built-in Controller method
        return Ok(todo);                    // Built-in Controller method
    }


    [HttpPost("todoitems")]
    public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
    {
        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();

        // CreatedAtAction automatically generates the URL for the new item
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }


    [HttpPut("todoitems/{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Todo inputTodo)
    {
        if (id != inputTodo.Id) return BadRequest();

        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        todo.Name = inputTodo.Name;
        todo.IsComplete = inputTodo.IsComplete;

        await _db.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("todoitems/{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
