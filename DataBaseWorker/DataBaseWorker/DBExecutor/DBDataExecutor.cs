using System.Data.Entity;
using DatabaseParser;
using IDataExecutor;

namespace DBExecutor
{
    public class DBDataExecutor : IDBDataExecutor
    {
        private readonly risTabulky aRisContext;

        public risTabulky risContext
        {
            get { return aRisContext; }
        }

        public DBDataExecutor()
        {
            aRisContext = new risTabulky();
        }
    }
}
