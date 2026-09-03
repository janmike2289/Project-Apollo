using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Infrastructure.Persistence.Repositories;

public sealed class CMItemRepository(AppDbContext dbContext)
{
    public async Task<IReadOnlyList<CMItemEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.CMItems
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<CMItemEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return dbContext.CMItems
            .AsNoTracking()
            .SingleOrDefaultAsync(changeManagement => changeManagement.Id == id, cancellationToken);
    }

    public async Task<CMItemEntity> AddAsync(
        CMItemEntity changeManagement,
        CancellationToken cancellationToken = default)
    {
        dbContext.CMItems.Add(changeManagement);
        await dbContext.SaveChangesAsync(cancellationToken);
        return changeManagement;
    }

    public async Task UpdateAsync(
        CMItemEntity changeManagement,
        CancellationToken cancellationToken = default)
    {
        dbContext.CMItems.Update(changeManagement);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var changeManagement = await dbContext.CMItems
            .SingleOrDefaultAsync(changeManagement => changeManagement.Id == id, cancellationToken);

        if (changeManagement is null)
        {
            return false;
        }

        dbContext.CMItems.Remove(changeManagement);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
