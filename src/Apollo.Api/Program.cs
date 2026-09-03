using System.ComponentModel.DataAnnotations;
using Apollo.Domain;
using Apollo.Domain.Repositories;
using Apollo.Infrastructure;
using Apollo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


// DB connection
// var connectionString = builder.Configuration.GetConnectionString("DevConnection") 
//     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(connectionString)); // Enable retry on failure for SQL Server

builder.Services.AddCors(); //add cors service
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // 2. Register native OpenAPI services


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();             // 3. Map the OpenAPI JSON endpoint (/openapi/v1.json)
    app.MapScalarApiReference(options =>
    {
        // Ensure this string perfectly mirrors your OpenAPI route structure
        options.WithOpenApiRoutePattern("/openapi/v1.json"); 
        options.Title = "My Custom API Docs";
        options.Theme = ScalarTheme.DeepSpace; // Choose from: Mars, Purple, DeepSpace, etc.
        options.Layout = ScalarLayout.Modern; // Standard or Modern layouts
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });  // 4. Map the Scalar UI endpoint (/scalar/v1)
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000", "https://localhost:5000"));

// custom endpoint for testing

// Mock In-Memory Database
var todos = new List<TodoItem>
{
    new(1, "Learn .NET 10", false),
    new(2, "Build a Minimal API", false)
};

// --- API ENDPOINTS ---

// GET: Fetch all items
app.MapGet("RM/todos", () => Results.Ok(todos));

// GET: Fetch a single item by ID
app.MapGet("RM/todos/{id:int}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

// POST: Create a new item with native .NET 10 parameter validation
app.MapPost("RM/todos", (CreateTodoRequest request) =>
{
    var newTodo = new TodoItem(todos.Count + 1, request.Title, false);
    todos.Add(newTodo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});


app.MapChangeManagementEndpoints(); // Map RM endpoints

app.Run();

// --- DATA MODELS ---

public record TodoItem(int Id, string Title, bool IsCompleted);

// Input model using standard attributes for automatic compilation-time validation
public record CreateTodoRequest(
    [Required(ErrorMessage = "Title is required")] 
    [StringLength(100, MinimumLength = 3)] 
    string Title
);


