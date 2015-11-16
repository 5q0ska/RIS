using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BNapoj
    {
        public int id_napoja { get; set; }
        public int nazov { get; set; }
        public int alkoholicky { get; set; }
        public int mnozstvo_kalorii { get; set; }
        public int dlzka_pripravy { get; set; }

        public ICollection<BMenu_napoj> menu_napoj { get; set; }
        public ICollection<BNapoj_surovina> napoj_surovina { get; set; }
        public BText text { get; set; }
        public ICollection<BTyp_napoja> typ_napoja { get; set; }

        private napoj entityNapoj;

        public BNapoj(napoj n)
        {
            id_napoja = n.id_napoja;
            nazov = n.nazov;
            alkoholicky = n.alkoholicky;
            mnozstvo_kalorii = (int) n.mnozstvo_kalorii;
            dlzka_pripravy = (int) n.dlzka_pripravy;
            text = new BText(n.text);

            menu_napoj = new List<BMenu_napoj>();
            foreach (var menuNapoj in n.menu_napoj)
            {
                BMenu_napoj pom = new BMenu_napoj(menuNapoj);
                menu_napoj.Add(pom);
            }
            napoj_surovina = new List<BNapoj_surovina>();
            foreach (var napojSurovina in n.napoj_surovina)
            {
                BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                napoj_surovina.Add(pom);
            }
            typ_napoja = new List<BTyp_napoja>();
            foreach (var typNapoja in n.typ_napoja)
            {
                BTyp_napoja pom = new BTyp_napoja(typNapoja);
                typ_napoja.Add(pom);
            }

            entityNapoj = n;

        }
    }
}
