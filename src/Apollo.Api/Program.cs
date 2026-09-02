using System.Text.Json.Serialization;
using Apollo.Api.Endpoints;
using Apollo.Domain.Entities;
using Apollo.Infrastructure;
using Apollo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


//--------------------------------------------default program builder for web app, do not touch-------------------------------------
var builder = WebApplication.CreateBuilder(args);

// -------------Services (Add the built in tools/components, and libraries as well as services to the application) -----------------

builder.Services.AddValidation(); // New in .NET 10 built-in endpoint validation
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi(); // modern api support
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddCors();

var app = builder.Build(); //Build the web api application

//-------------------------------------------------------Middleware Pipeline------------------------------------------------------------------------
//Note: The order of the middleware is important. The middleware is executed in the order that it is added to the pipeline.




//CORS is an HTTP-header based mechanism that allows a server to indicate any origins (domain, scheme, or port) other than its own from which a browser should permit loading resources
//In this case, we are allowing the client (react) to access resource if it is coming from the origin of 4200
//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapChangeTicketEndpoints();

app.Run();
