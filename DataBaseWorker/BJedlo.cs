using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BJedlo
    {
        public int id_jedla { get; set; }
        public int nazov { get; set; }
        public int id_typu { get; set; }
        public int mnozstvo_kalorii { get; set; }
        public int dlzka_pripravy { get; set; }

        public ICollection<BMenu_jedlo> menu_jedlo { get; set; }
        public ICollection<BJedlo_surovina> jedlo_surovina { get; set; }
        
        public BTyp_jedla typ_jedla { get; set; }
        public BText text { get; set; }

        private jedlo entityJedlo;

        public BJedlo(BManager manager,jedlo j)
        {
            id_jedla = j.id_jedla;
            nazov = j.nazov;
            id_typu = j.id_typu;
            if (j.mnozstvo_kalorii != null) mnozstvo_kalorii = (int) j.mnozstvo_kalorii;
            if (j.dlzka_pripravy != null) dlzka_pripravy = (int) j.dlzka_pripravy;
            menu_jedlo = new List<BMenu_jedlo>();
            foreach (var menuJedlo in j.menu_jedlo)
            {
                BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                menu_jedlo.Add(pom);
            }

            jedlo_surovina = new List<BJedlo_surovina>();
            foreach (var jedloSurovina in j.jedlo_surovina)
            {
                BJedlo_surovina pom = new BJedlo_surovina(jedloSurovina);
                jedlo_surovina.Add(pom);
            }

            typ_jedla = new BTyp_jedla(j.typ_jedla);
            this.text = new BText(j.text);

            entityJedlo = j;
        }
    }
}
