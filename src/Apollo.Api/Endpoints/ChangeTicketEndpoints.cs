using Apollo.Domain.Entities;
using Apollo.Domain.Repositories;
using Apollo.Domain.Specifications;

namespace Apollo.Api.Endpoints;

public static class ChangeTicketEndpoints
{
    public static IEndpointRouteBuilder MapChangeTicketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/change-tickets").WithTags("Change Tickets");

        group.MapGet("/", ListAsync);
        group.MapGet("/sql", ListWithDapperAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost("/", CreateAsync);
        group.MapPut("/{id:guid}", UpdateAsync);
        group.MapDelete("/{id:guid}", DeleteAsync);
        group.MapPost("/{id:guid}/change-log", AddChangeLogCommentAsync);
        group.MapPost("/{id:guid}/attachments/email", AttachEmailAsync);
        group.MapPost("/{id:guid}/attachments/screenshots", AttachScreenshotAsync);

        return app;
    }

    private static async Task<IResult> ListAsync(
        IChangeTicketRepository repository,
        string? title,
        ChangeStatus? status,
        ChangeType? changeType,
        string? requester,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default)
    {
        var spec = new ChangeTicketsQuerySpec(title, status, changeType, requester, skip, take);
        var items = await repository.ListAsync(spec, cancellationToken);
        return Results.Ok(items.Select(ChangeTicketResponse.From));
    }

    private static async Task<IResult> ListWithDapperAsync(
        IDapperQuery dapper,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Id, Title, Description, ChangeType, Status, Priority, Requester, AssignedTo, UpdatedAt
            FROM ChangeTickets
            ORDER BY UpdatedAt DESC
            """;

        var items = await dapper.QueryAsync<ChangeTicketRow>(sql, cancellationToken: cancellationToken);
        return Results.Ok(items);
    }

    private static async Task<IResult> GetByIdAsync(
        Guid id,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var ticket = await repository.GetByIdAsync(id, cancellationToken);
        return ticket is null ? Results.NotFound() : Results.Ok(ChangeTicketResponse.From(ticket));
    }

    private static async Task<IResult> CreateAsync(
        CreateChangeTicketRequest request,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var ticket = new ChangeTicket(
            request.Title,
            request.Description,
            request.ChangeType,
            request.Priority,
            request.Requester,
            request.AssignedTo,
            request.ImplementationPlan,
            request.RollbackPlan,
            request.ScheduledStart,
            request.ScheduledEnd);

        await repository.CreateAsync(ticket, cancellationToken);
        return Results.Created($"/change-tickets/{ticket.Id}", ChangeTicketResponse.From(ticket));
    }

    private static async Task<IResult> UpdateAsync(
        Guid id,
        UpdateChangeTicketRequest request,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var ticket = await repository.GetByIdAsync(id, cancellationToken);
        if (ticket is null)
        {
            return Results.NotFound();
        }

        ticket.Update(
            request.Title,
            request.Description,
            request.ChangeType,
            request.Status,
            request.Priority,
            request.Requester,
            request.AssignedTo,
            request.ImplementationPlan,
            request.RollbackPlan,
            request.ScheduledStart,
            request.ScheduledEnd);

        await repository.UpdateAsync(ticket, cancellationToken);
        return Results.Ok(ChangeTicketResponse.From(ticket));
    }

    private static async Task<IResult> DeleteAsync(
        Guid id,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AddChangeLogCommentAsync(
        Guid id,
        AddChangeLogCommentRequest request,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var entry = await repository.AddChangeLogCommentAsync(id, request.Body, request.Author, cancellationToken);
        return entry is null
            ? Results.NotFound()
            : Results.Created($"/change-tickets/{id}/change-log/{entry.Id}", ChangeLogCommentResponse.From(entry));
    }

    private static async Task<IResult> AttachEmailAsync(
        Guid id,
        AttachEmailRequest request,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var attachment = await repository.AttachEmailAsync(
            id,
            request.FileName,
            request.StorageKey,
            request.EmailFrom,
            request.EmailSubject,
            request.ContentType ?? "message/rfc822",
            cancellationToken);

        return attachment is null
            ? Results.NotFound()
            : Results.Created($"/change-tickets/{id}/attachments/{attachment.Id}", AttachmentResponse.From(attachment));
    }

    private static async Task<IResult> AttachScreenshotAsync(
        Guid id,
        AttachScreenshotRequest request,
        IChangeTicketRepository repository,
        CancellationToken cancellationToken)
    {
        var attachment = await repository.AttachScreenshotAsync(
            id,
            request.FileName,
            request.ContentType,
            request.StorageKey,
            cancellationToken);

        return attachment is null
            ? Results.NotFound()
            : Results.Created($"/change-tickets/{id}/attachments/{attachment.Id}", AttachmentResponse.From(attachment));
    }
}

public sealed record CreateChangeTicketRequest(
    string Title,
    string Description,
    ChangeType ChangeType,
    ChangePriority Priority,
    string Requester,
    string? AssignedTo,
    string? ImplementationPlan,
    string? RollbackPlan,
    DateTimeOffset? ScheduledStart,
    DateTimeOffset? ScheduledEnd);

public sealed record UpdateChangeTicketRequest(
    string Title,
    string Description,
    ChangeType ChangeType,
    ChangeStatus Status,
    ChangePriority Priority,
    string Requester,
    string? AssignedTo,
    string? ImplementationPlan,
    string? RollbackPlan,
    DateTimeOffset? ScheduledStart,
    DateTimeOffset? ScheduledEnd);

public sealed record AddChangeLogCommentRequest(string Body, string Author);

public sealed record AttachEmailRequest(
    string FileName,
    string StorageKey,
    string? EmailFrom,
    string? EmailSubject,
    string? ContentType);

public sealed record AttachScreenshotRequest(string FileName, string ContentType, string StorageKey);

public sealed record ChangeLogCommentResponse(Guid Id, string Body, string Author, DateTimeOffset CreatedAt)
{
    public static ChangeLogCommentResponse From(ChangeLogComment comment) =>
        new(comment.Id, comment.Body, comment.Author, comment.CreatedAt);
}

public sealed record AttachmentResponse(
    Guid Id,
    AttachmentKind Kind,
    string FileName,
    string ContentType,
    string StorageKey,
    string? EmailFrom,
    string? EmailSubject,
    DateTimeOffset CreatedAt)
{
    public static AttachmentResponse From(Attachment attachment) =>
        new(
            attachment.Id,
            attachment.Kind,
            attachment.FileName,
            attachment.ContentType,
            attachment.StorageKey,
            attachment.EmailFrom,
            attachment.EmailSubject,
            attachment.CreatedAt);
}

public sealed record ChangeTicketResponse(
    Guid Id,
    string Title,
    string Description,
    ChangeType ChangeType,
    ChangeStatus Status,
    ChangePriority Priority,
    string Requester,
    string? AssignedTo,
    string? ImplementationPlan,
    string? RollbackPlan,
    DateTimeOffset? ScheduledStart,
    DateTimeOffset? ScheduledEnd,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    IReadOnlyList<ChangeLogCommentResponse> ChangeLog,
    IReadOnlyList<AttachmentResponse> Attachments)
{
    public static ChangeTicketResponse From(ChangeTicket ticket) =>
        new(
            ticket.Id,
            ticket.Title,
            ticket.Description,
            ticket.ChangeType,
            ticket.Status,
            ticket.Priority,
            ticket.Requester,
            ticket.AssignedTo,
            ticket.ImplementationPlan,
            ticket.RollbackPlan,
            ticket.ScheduledStart,
            ticket.ScheduledEnd,
            ticket.CreatedAt,
            ticket.UpdatedAt,
            ticket.ChangeLog.Select(ChangeLogCommentResponse.From).ToList(),
            ticket.Attachments.Select(AttachmentResponse.From).ToList());
}

public sealed record ChangeTicketRow(
    Guid Id,
    string Title,
    string Description,
    string ChangeType,
    string Status,
    string Priority,
    string Requester,
    string? AssignedTo,
    DateTimeOffset UpdatedAt);
