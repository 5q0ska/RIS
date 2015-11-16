using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BUcet
    {
        public int id_uctu { get; set; }
        public int id { get; set; }
        public string login { get; set; }
        public string heslo { get; set; }

        public ICollection<BObjednavka> objednavka { get; set; }
        public BTyp_uctu typ_uctu { get; set; }

        private ucet entityUcet;

        public BUcet(ucet u)
        {
            id_uctu = u.id_uctu;
            id = u.id;
            login = u.login;
            heslo = u.heslo;
            typ_uctu = new BTyp_uctu(u.typ_uctu);

            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in u.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
            entityUcet = u;
        }
    }
}
