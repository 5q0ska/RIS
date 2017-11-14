using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TObjednavka
    {
        [DataMember]
        public int? Id { get; set; }
        
        [DataMember]
        public int Accepted { get; set; }

        [DataMember]
        public int Table { get; set; }

        [DataMember]
        public int Account { get; set; }

        [DataMember]
        public double Sum { get; set; }

        public TObjednavka(int id_objednavky, int id_stola, int id_uctu, int potvrdena, double suma)
        {
            this.Id = id_objednavky;
            this.Table = id_stola;
            this.Account = id_uctu;
            this.Accepted = potvrdena;
            this.Sum = suma;
        }
    }
}
