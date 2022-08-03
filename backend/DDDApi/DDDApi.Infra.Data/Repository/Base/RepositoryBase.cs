using DDDApi.Domain.Core.Interfaces.Repository.Base;
using DDDApi.Domain.Core.Entities.Base;
using DDDApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDApi.Infra.Data.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly IEntityContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public RepositoryBase(IEntityContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var r = await dbSet.AddAsync(entity, cancellationToken);
            await CommitAsync(cancellationToken);

            return r.Entity;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await dbSet.AddRangeAsync(entities, cancellationToken);
            return await CommitAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
            => await dbSet.ToListAsync(cancellationToken);

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => await dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken) 
            => await dbSet.AnyAsync(x => x.Id == id, cancellationToken);

        public async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            dbSet.Remove(entity);
            return await CommitAsync(cancellationToken);
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            dbSet.RemoveRange(entities);
            return await CommitAsync(cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var r = dbSet.Update(entity);
            await CommitAsync(cancellationToken);
            return r.Entity;
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            dbSet.UpdateRange(entities);
            return await CommitAsync(cancellationToken);
        }

        private async Task<int> CommitAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
