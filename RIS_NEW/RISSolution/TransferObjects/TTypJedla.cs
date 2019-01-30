using System;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TTypJedla
    {
        int id;
        String typ;

        
        public TTypJedla(int id, string typ)
        {
            this.id = id;
            this.typ = typ;
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }
    }
}
