using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;
using DBExecutor;
using ICommunicationDMDB;
using IDataExecutor;

namespace DirectCommunication
{
    public class DirectCommDMDB : ICommunication
    {
        private IDBDataExecutor aDBExecutor = new DBDataExecutor();

        public risTabulky risContext
        {
            get { return aDBExecutor.risContext; }
        }

        public IList jedloList { get; private set; }
        //get { return risContext.jedlo; } ;
        //}
    }
}
