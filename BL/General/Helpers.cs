using System.Transactions;

namespace Web_siteResume.BL.General
{
    public static class Helpers //хелпер-враппер
    {
        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

        public static TransactionScope CreateTransactionScope(int seconds = 6000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );

        }

    }
}
