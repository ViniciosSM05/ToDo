using DDDApi.Domain.Core.Interfaces.Repository;
using DDDApi.Domain.Core.Entities;
using DDDApi.Infra.Data.Context;
using DDDApi.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using DDDApi.Infra.CrossCutting.Security;

namespace DDDApi.Infra.Data.Repository
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser(IEntityContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> ExistsEmailAsync(string email, CancellationToken cancellationToken) 
            => await dbSet.AnyAsync(x => x.Email == email, cancellationToken);

        public async Task<User> GetByCredentialsAsync(string email, string password, CancellationToken cancellationToken)
            => await dbSet.FirstOrDefaultAsync(x => x.Email == email && x.Password == Cryptography.Execute(password), cancellationToken);
    }
}
