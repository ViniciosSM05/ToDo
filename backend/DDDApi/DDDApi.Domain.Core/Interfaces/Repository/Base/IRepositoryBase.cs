using DDDApi.Domain.Core.Entities.Base;

namespace DDDApi.Domain.Core.Interfaces.Repository.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }
}
