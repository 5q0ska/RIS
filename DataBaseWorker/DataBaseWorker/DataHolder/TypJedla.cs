using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataHolder
{
    [DataContract]
    public class TypJedla
    {
        int id;
        String typ;

        
        public TypJedla(int id, string typ)
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
