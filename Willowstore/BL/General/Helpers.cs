using System.Transactions;

namespace Willowstore.BL.General
{
    public class Helpers
    {
        // try to parse or default
        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if(int.TryParse(str, out value))
                return value;
            return def;
        }

        public static Guid? StringToGuidDef(string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value)) 
                return value;
            return null;
        }

        public static TransactionScope CreateTransactionScope(int seconds = 1000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
