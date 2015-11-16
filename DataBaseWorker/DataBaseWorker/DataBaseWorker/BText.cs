using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BText
    {
        public int text_id { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDen_v_tyzdni> den_v_tyzdni { get; set; }
        public ICollection<BJedlo> jedlo { get; set; }
        public ICollection<BMenu> menu { get; set; }
        public ICollection<BMenu> menu1 { get; set; }
        public ICollection<BNapoj> napoj { get; set; }
        public ICollection<BPreklad> preklad { get; set; }
        public ICollection<BSurovina> surovina { get; set; }
        public ICollection<BTyp_jedla> typ_jedla { get; set; }
        public ICollection<BTyp_napoja> typ_napoja { get; set; }

        private text entityText;

        public BText(text t)
        {
            text_id = t.text_id;
            akcia = new List<BAkcia>();
            foreach (var akcia1 in t.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            den_v_tyzdni = new List<BDen_v_tyzdni>();
            foreach (var denVTyzdni in t.den_v_tyzdni)
            {
                BDen_v_tyzdni pom = new BDen_v_tyzdni(denVTyzdni);
                den_v_tyzdni.Add(pom);
            }
            jedlo = new List<BJedlo>();
            foreach (var jedlo1 in t.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }
            menu = new List<BMenu>();
            foreach (var menu2 in t.menu)
            {
                BMenu pom = new BMenu(menu2);
                menu.Add(pom);
            }
            menu1 = new List<BMenu>();
            foreach (var menu2 in t.menu1)
            {
                BMenu pom = new BMenu(menu2);
                menu1.Add(pom);
            }
            napoj = new List<BNapoj>();
            foreach (var napoj1 in t.napoj)
            {
                BNapoj pom = new BNapoj(napoj1);
                napoj.Add(pom);
            }
            preklad = new List<BPreklad>();
            foreach (var preklad1 in t.preklad)
            {
                BPreklad pom = new BPreklad(preklad1);
                preklad.Add(pom);
            }
            surovina = new List<BSurovina>();
            foreach (var surovina1 in t.surovina)
            {
                BSurovina pom = new BSurovina(surovina1);
                surovina.Add(pom);
            }
            typ_jedla = new List<BTyp_jedla>();
            foreach (var typJedla in t.typ_jedla)
            {
                BTyp_jedla pom = new BTyp_jedla(typJedla);
                typ_jedla.Add(pom);
            }
            typ_napoja = new List<BTyp_napoja>();
            foreach (var typNapoja in t.typ_napoja)
            {
                BTyp_napoja pom = new BTyp_napoja(typNapoja);
                typ_napoja.Add(pom);
            }

            entityText = t;
        }
    }
}
