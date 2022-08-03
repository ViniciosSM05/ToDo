using DDDApi.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DDDApi.Infra.Data.Context
{
    public interface IEntityContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Todo> Todos { get; set; }

        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        new void Dispose();
        ValueTask DisposeAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
