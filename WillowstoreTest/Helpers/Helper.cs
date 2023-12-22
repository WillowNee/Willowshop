using System.Transactions;

namespace WillowstoreTest.Helpers
{
    // Common configuration
    public static class Helper
    {
        public static TransactionScope CreateTransactionScope (int seconds = 99999)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
