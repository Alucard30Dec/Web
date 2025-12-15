using Lab05.Data;
using Lab05.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ================== SERVICES ==================

// EF Core InMemory
builder.Services.AddDbContext<TodoDb>(options =>
    options.UseInMemoryDatabase("TodoList"));

// Hiện lỗi EF đẹp hơn trong môi trường Development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Cấu hình JSON cho Minimal API
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.WriteIndented = true;
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Lab05 Todo API",
        Version = "v1"
    });
});

var app = builder.Build();

// ================== PIPELINE ==================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Chỉ rõ swagger.json để tránh trỏ nhầm
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab05 Todo API v1");
    });
}

app.UseHttpsRedirection();

// Endpoint test gốc
app.MapGet("/", () => "Hello from Lab05!");

// Hàm helper: map entity -> DTO (không dùng trong Select)
static TodoItemDTO ToDto(Todo todo) =>
    new()
    {
        Id = todo.Id,
        Name = todo.Name,
        IsComplete = todo.IsComplete
    };

// ================== ENDPOINTS CRUD ==================

// GET /todoitems
app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos
        .Select(t => new TodoItemDTO
        {
            Id = t.Id,
            Name = t.Name,
            IsComplete = t.IsComplete
        })
        .ToListAsync());

// GET /todoitems/complete
app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos
        .Where(t => t.IsComplete)
        .Select(t => new TodoItemDTO
        {
            Id = t.Id,
            Name = t.Name,
            IsComplete = t.IsComplete
        })
        .ToListAsync());

// GET /todoitems/{id}
app.MapGet("/todoitems/{id:int}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
        return Results.NotFound();

    return Results.Ok(ToDto(todo));
});

// POST /todoitems
app.MapPost("/todoitems", async (TodoItemDTO dto, TodoDb db) =>
{
    var todo = new Todo
    {
        Name = dto.Name,
        IsComplete = dto.IsComplete,
        // Client không set được Secret
        Secret = "server-only-secret"
    };

    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    var resultDto = ToDto(todo);

    return Results.Created($"/todoitems/{todo.Id}", resultDto);
});

// PUT /todoitems/{id}
app.MapPut("/todoitems/{id:int}", async (int id, TodoItemDTO dto, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
        return Results.NotFound();

    todo.Name = dto.Name;
    todo.IsComplete = dto.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// DELETE /todoitems/{id}
app.MapDelete("/todoitems/{id:int}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
        return Results.NotFound();

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();

    return Results.Ok(ToDto(todo));
});

app.Run();
