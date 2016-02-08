using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHolder;

namespace DataBaseWorker
{
    /// <summary>
    /// Rozhranie pre prenosovú entitu
    /// </summary>
    public interface TransferTemplate
    {
         TransferEntity toTransferObject(String id_jazyka);
    }

    /// <summary>
    /// Rozhranie pre zoznam prenosových entít
    /// </summary>
    public interface TransferTemplateList
    {
        IList<TransferEntity> toTransferList(String id_jazyka);
    }
}
