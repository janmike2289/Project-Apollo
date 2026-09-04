using Apollo.Infrastructure.Persistence;

public static class EPTestEndpoint
{
    public static void MapTestEndpoints(this IEndpointRouteBuilder app)
    {
        // Grouping routes to prefix with /api/test
        var group = app.MapGroup("/api/test")
                       .WithDescription("Endpoints for testing and demonstration purposes")
                       .WithTags("test"); // Automatically groups endpoints in OpenAPI/Swagger

        group.MapGet("/test-DB", TestDatabaseConnection)
             .WithName("TestDatabaseConnection")
             .WithSummary("Tests DB Connection");
            //  .Produces<string>(StatusCodes.Status200OK)
            //  .ProducesProblem(StatusCodes.Status503ServiceUnavailable);

        // GET all test items
        group.MapGet("/", GetAllTestItems);

        // GET test item by ID (using route constraint)
        group.MapGet("/{id:int}", GetTestItemById);

        // POST a new test item
        group.MapPost("/", CreateTestItem);
    }

    private static async Task<IResult> TestDatabaseConnection(AppDbContext db)
    {
        var canConnect = await db.Database.CanConnectAsync();
    
        return canConnect 
            ? Results.Ok("Database connection is working!") 
            : Results.Problem("Cannot connect to the database.", statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    private static IResult GetAllTestItems()
    {
        var testItems = new[] { 
            new { Id = 1, Name = "Test Item 1" }, 
            new { Id = 2, Name = "Test Item 2" },
            new { Id = 3, Name = "Test Item 3" },
            new { Id = 4, Name = "Test Item 4" },
            new { Id = 5, Name = "Test Item 5" } 
        };
        return Results.Ok(testItems);
    }

    private static IResult GetTestItemById(int id)
    {
        if (id != 1) return Results.NotFound();
        return Results.Ok(new { Id = 1, Name = "Test Item 1" });
    }

    private static IResult CreateTestItem(TestItemDto dto)
    {
        // Process your data here...
        return Results.Created($"/api/test/1", new { Id = 1, dto.Name });
    }
}