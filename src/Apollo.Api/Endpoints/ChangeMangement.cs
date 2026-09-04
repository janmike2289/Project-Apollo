using Apollo.Domain.Entities;
using Apollo.Domain.Interface;
using Apollo.Infrastructure.Persistence.Repositories;


public static class EPChangeMangement
{
    public static void MapChangeManagementEndpoints(this IEndpointRouteBuilder app)
    {
        // Grouping routes to prefix with /api/products
        var group = app.MapGroup("/v1/rm")
                       .WithTags("rm"); // Automatically groups endpoints in OpenAPI/Swagger

        // GET all RM
        group.MapGet("/", GetAllRM);

        // GET RM by ID (using route constraint)
        group.MapGet("/{id:int}", GetRMById);

        // POST a new RM ticket 
        group.MapPost("/", CreateRM);
    }

    //get all RM tickets
    private static async Task<IResult> GetAllRM(ICMItemRepository repository, CancellationToken ct)
    {
        return Results.Ok(await repository.GetAllAsync(ct));
    }
    
    //get RM ticket by id
    private static async Task<IResult> GetRMById(int id, ICMItemRepository repository, CancellationToken ct)
    {
        var rm = await repository.GetByIdAsync(id, ct);
        return rm is not null ? Results.Ok(rm) : Results.NotFound();
    }

    //Create new RM ticket
    private static async Task<IResult> CreateRM(CMItemEntity rm, ICMItemRepository repository, CancellationToken ct)
    {
        var id = await repository.CreateAsync(rm, ct);
        
        rm.Id = id;
        return Results.Created($"/v1/rm/{id}", rm);
    }
}