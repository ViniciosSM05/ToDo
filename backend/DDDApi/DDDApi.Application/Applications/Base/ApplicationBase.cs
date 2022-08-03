using DDDApi.Domain.Core.Interfaces.Application.Base;
using System.Transactions;

namespace DDDApi.Application.Applications.Base
{
    public abstract class ApplicationBase : IApplicationBase
    {
        public TransactionScope TransactionScope() => new();
        public TransactionScope TransactionScopeAsync() => new(TransactionScopeAsyncFlowOption.Enabled);
    }
}
