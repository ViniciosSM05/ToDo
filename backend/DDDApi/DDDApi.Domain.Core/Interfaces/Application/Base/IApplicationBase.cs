using System.Transactions;

namespace DDDApi.Domain.Core.Interfaces.Application.Base
{
    public interface IApplicationBase
    {
        TransactionScope TransactionScope();
        TransactionScope TransactionScopeAsync();
    }
}
