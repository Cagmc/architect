using System.Transactions;

namespace Architect.Common.Infrastructure
{
    public static class TransactionFactory
    {
        public static TransactionScope CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = isolationLevel },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
