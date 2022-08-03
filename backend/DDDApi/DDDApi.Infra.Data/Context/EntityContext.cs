using DDDApi.Domain.Core.Entities;
using DDDApi.Domain.Core.Entities.Base;
using DDDApi.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DDDApi.Infra.Data.Context
{
    public class EntityContext : DbContext, IEntityContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        #region CONFIGS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MappingUser());
            modelBuilder.ApplyConfiguration(new MappingTodo());
        }

        public override int SaveChanges()
        {
            SetDates();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetDates()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity);
            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity is null) continue;

                if (entry.State == EntityState.Added)
                    entity.InData = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                {
                    entity.MoData = DateTime.UtcNow;
                    entry.Property(nameof(entity.InData)).IsModified = false;
                }
            }
        }
        #endregion CONFIGS
    }
}
