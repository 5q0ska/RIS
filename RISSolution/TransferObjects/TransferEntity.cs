using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataHolder
{
    [DataContract]
    public abstract class TransferEntity
    {
        String id_jazyka;

        [DataMember]
        public string Id_jazyka
        {
            get
            {
                return id_jazyka;
            }

            set
            {
                id_jazyka = value;
            }
        }
    }
}
