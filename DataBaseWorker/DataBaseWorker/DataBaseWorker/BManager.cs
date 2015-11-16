using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBExecutor;
using ICommunicationDMDB;
using IDataExecutor;

namespace DataBaseWorker
{
    public class BManager
    {
        private ICommunication aComm;
        private readonly List<BJedlo> aJedloList = null;

        public List<BJedlo> jedloList
        {
            get
            {
                if (aJedloList == null)
                {
                    IList list = aComm.jedloList;
                    //napln aJedloList z list
                }
                return aJedloList;
            }
        }
    }
}
