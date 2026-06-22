var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root Path");

app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) =>
{
    return $"User id: {userId}, Post id: {slug}";
});

app.MapGet("/products/{id:int:min(0)}", (int id) =>
{
    return $"product id: {id}";
});

app.MapGet("/report/{year?}", (int? year = 2018) =>
{
    return $"Year: {year}";
});

app.MapGet("/files/{*filePath}", (string filePath) =>
{
    return $"File: {filePath}"; 
});

app.MapGet("/search", (string? q, int page = 1) =>
{
    return $"searching for {q} on page {page}";
});



app.Run();