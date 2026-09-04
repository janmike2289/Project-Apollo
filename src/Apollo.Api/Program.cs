using Apollo.Domain.Interface;
using Apollo.Infrastructure.Persistence;
using Apollo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


//DB connection
var connectionString = builder.Configuration.GetConnectionString("DevConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Enable retry on failure for SQL Server

builder.Services.AddCors(); //add cors service
builder.Services.AddOpenApi(); // 2. Register native OpenAPI services
builder.Services.AddValidation(); // Register built-in Minimal API validation

//---------------------------------------Register the repositories-----------------------------------
builder.Services.AddScoped<ICMItemRepository, CMItemRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();             //Map the OpenAPI JSON endpoint (/openapi/v1.json)
    app.MapScalarApiReference(options =>
    {
        // Ensure this string perfectly mirrors your OpenAPI route structure
        options.WithOpenApiRoutePattern("/openapi/v1.json"); 
        options.Title = "My Custom API Docs";
        options.Theme = ScalarTheme.Kepler; // Choose from: Mars, Purple, DeepSpace, etc.
        options.Layout = ScalarLayout.Modern; // Standard or Modern layouts
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });  // 4. Map the Scalar UI endpoint (/scalar/v1)
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000", "https://localhost:5000"));


//--------------------Endpoints----------------------------------

app.MapChangeManagementEndpoints(); // Map RM endpoints
app.MapTestEndpoints(); // Map Test endpoints

app.Run();






