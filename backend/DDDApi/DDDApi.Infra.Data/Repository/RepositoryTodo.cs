using DDDApi.Domain.Core.Interfaces.Repository;
using DDDApi.Domain.Core.Entities;
using DDDApi.Infra.Data.Context;
using DDDApi.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using DDDApi.Domain.Core.DTO.Todo;

namespace DDDApi.Infra.Data.Repository
{
    public class RepositoryTodo : RepositoryBase<Todo>, IRepositoryTodo
    {
        public RepositoryTodo(IEntityContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> ExistsCodeAsync(string code, Guid userId, CancellationToken cancellationToken) 
            => await dbSet.AnyAsync(x => x.Code == code && x.UserId == userId, cancellationToken);

        public async Task<bool> ExistsCodeExceptIdAsync(Guid exceptId, string code, Guid userId, CancellationToken cancellationToken)
            => await dbSet.AnyAsync(x => x.Id != exceptId && x.Code == code && x.UserId == userId, cancellationToken);

        public async Task<List<TodoGridDTO>> GetTodosByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = (
                from todo in dbSet.AsQueryable()
                where todo.UserId.Equals(userId)
                select new TodoGridDTO
                {
                    Id = todo.Id,
                    Code = todo.Code,
                    Description = todo.Description,
                    Date = todo.Date,
                }
            );

            return await query.ToListAsync(cancellationToken);
        }
    }
}
