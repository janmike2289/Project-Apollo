public static class EPTestEndpoint
{
    public static void MapTestEndpoints(this IEndpointRouteBuilder app)
    {
        // Grouping routes to prefix with /api/test
        var group = app.MapGroup("/api/test")
                       .WithTags("test"); // Automatically groups endpoints in OpenAPI/Swagger

        // GET all test items
        group.MapGet("/", GetAllTestItems);

        // GET test item by ID (using route constraint)
        group.MapGet("/{id:int}", GetTestItemById);

        // POST a new test item
        group.MapPost("/", CreateTestItem);
    }

    // Handlers can be written as private static methods below
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