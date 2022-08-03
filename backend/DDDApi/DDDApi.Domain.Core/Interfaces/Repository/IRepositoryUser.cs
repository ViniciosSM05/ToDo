using DDDApi.Domain.Core.Interfaces.Repository.Base;
using DDDApi.Domain.Core.Entities;

namespace DDDApi.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        Task<bool> ExistsEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetByCredentialsAsync(string email, string password, CancellationToken cancellationToken);
    }
}
