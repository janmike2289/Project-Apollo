using Apollo.Domain.Entities;
using Apollo.Domain.Repositories;
using Apollo.Domain.Specifications;
using Apollo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", ListAsync);
        group.MapGet("/sql", ListWithDapperAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost("/", CreateAsync);
        group.MapPut("/{id:guid}", UpdateAsync);
        group.MapDelete("/{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<IResult> ListAsync(
        IRepository<Product> repository,
        string? name,
        bool? isActive,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default)
    {
        var spec = new ProductsQuerySpec(name, isActive, skip, take);
        var items = await repository.ListAsync(spec, cancellationToken);
        return Results.Ok(items.Select(ProductResponse.From));
    }

    private static async Task<IResult> ListWithDapperAsync(
        IDapperQuery dapper,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Id, Name, Price, IsActive
            FROM Products
            ORDER BY Name
            """;

        var items = await dapper.QueryAsync<ProductRow>(sql, cancellationToken: cancellationToken);
        return Results.Ok(items);
    }

    private static async Task<IResult> GetByIdAsync(
        Guid id,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = await repository.FirstOrDefaultAsync(new ProductByIdSpec(id), cancellationToken);
        return product is null ? Results.NotFound() : Results.Ok(ProductResponse.From(product));
    }

    private static async Task<IResult> CreateAsync(
        CreateProductRequest request,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price);
        await repository.AddAsync(product, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Results.Created($"/products/{product.Id}", ProductResponse.From(product));
    }

    private static async Task<IResult> UpdateAsync(
        Guid id,
        UpdateProductRequest request,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = await repository.FirstOrDefaultAsync(new ProductByIdSpec(id), cancellationToken);
        if (product is null)
        {
            return Results.NotFound();
        }

        product.Update(request.Name, request.Price, request.IsActive);
        repository.Update(product);
        await repository.SaveChangesAsync(cancellationToken);
        return Results.Ok(ProductResponse.From(product));
    }

    private static async Task<IResult> DeleteAsync(
        Guid id,
        IRepository<Product> repository,
        CancellationToken cancellationToken)
    {
        var product = await repository.FirstOrDefaultAsync(new ProductByIdSpec(id), cancellationToken);
        if (product is null)
        {
            return Results.NotFound();
        }

        repository.Remove(product);
        await repository.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}

public sealed record CreateProductRequest(string Name, decimal Price);

public sealed record UpdateProductRequest(string Name, decimal Price, bool IsActive);

public sealed record ProductResponse(Guid Id, string Name, decimal Price, bool IsActive)
{
    public static ProductResponse From(Product product) =>
        new(product.Id, product.Name, product.Price, product.IsActive);
}

public sealed record ProductRow(Guid Id, string Name, decimal Price, bool IsActive);
