using Apollo.Domain.Entities;

namespace Apollo.Domain.Interface
{
    public interface ICMItemRepository
    {
        Task<IEnumerable<CMItemEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CMItemEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CMItemEntity changeManagement, CancellationToken cancellationToken = default);
        Task<CMItemEntity> AddAsync(CMItemEntity changeManagement, CancellationToken cancellationToken = default);
        Task UpdateAsync(CMItemEntity changeManagement, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}