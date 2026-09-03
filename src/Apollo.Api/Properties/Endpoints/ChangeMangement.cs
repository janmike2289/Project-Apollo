public static class EPChangeMangement
{
    public static void MapChangeManagementEndpoints(this IEndpointRouteBuilder app)
    {
        // Grouping routes to prefix with /api/products
        var group = app.MapGroup("/api/rm")
                       .WithTags("rm"); // Automatically groups endpoints in OpenAPI/Swagger

        // GET all RM
        group.MapGet("/", GetAllRM);

        // GET RM by ID (using route constraint)
        group.MapGet("/{id:int}", GetRMById);

        // POST a new RM ticket 
        group.MapPost("/", CreateRM);
    }

    // Handlers can be written as private static methods below
    private static IResult GetAllRM()
    {
        var rms = new[] { new { Id = 1, Name = "Laptop" }, new { Id = 2, Name = "Mouse" } };
        return Results.Ok(rms);
    }

    private static IResult GetRMById(int id)
    {
        if (id != 1) return Results.NotFound();
        return Results.Ok(new { Id = 1, Name = "Laptop" });
    }

    private static IResult CreateRM(CMItemDto dto)
    {
        // Process your data here...
        return Results.Created($"/api/rm/1", new { Id = 1, dto.Name });
    }
}