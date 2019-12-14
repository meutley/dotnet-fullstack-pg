using System.Transactions;

namespace SourceName.Service.Implementation
{
    public static class AmbientTransaction
    {
        public static TransactionScope Create()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }
            );
        }
    }
}