using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TransferObjects
{
    [DataContract]
    public class TObjednavkaMenu
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int Objednavka { get; set; }

        [DataMember]
        public int Podnik { get; set; }

        [DataMember]
        public int Menu { get; set; }

        [DataMember]
        public int Jedlo { get; set; }

        [DataMember]
        public int Mnozstvo { get; set; }

        public TObjednavkaMenu(int? id, int objednavka, int podnik, int menu, int jedlo, int mnozstvo)
        {
            Id = id;
            Objednavka = objednavka;
            Podnik = podnik;
            Menu = menu;
            Jedlo = jedlo;
            Mnozstvo = mnozstvo;
        }

        public TObjednavkaMenu(int objednavka, int podnik, int menu, int jedlo)
        {
            Objednavka = objednavka;
            Podnik = podnik;
            Menu = menu;
            Jedlo = jedlo;
        }
    }
}
