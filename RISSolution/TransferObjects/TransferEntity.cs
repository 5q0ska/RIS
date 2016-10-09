using System;
using System.Runtime.Serialization;

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
