using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    internal interface IBAkcia
    {
        int id_akcie { get; set; }
        int id_menu { get; set; }
        int id_podniku { get; set; }
        int id_obrazka { get; set; }
        int text_id { get; set; }
        DateTime platnost_od { get; set; }
        DateTime platnost_do { get; set; }
        int akciova_cena { get; set; }
        BText text { get; set; }
        BMenu menu { get; set; }
        BObrazok obrazok { get; set; }
    }

    public class BAkcia : IBAkcia
    {
        public int id_akcie { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int text_id { get; set; }
        public DateTime platnost_od { get; set; }
        public DateTime platnost_do { get; set; }
        public int akciova_cena { get; set; }

        public BText text { get; set; }
        public BMenu menu { get; set; }
        public BObrazok obrazok { get; set; }

        private akcia entityAkcia;

        public BAkcia(akcia akcia)
        {
            id_akcie = akcia.id_akcie;
            id_menu = akcia.id_menu;
            id_podniku = akcia.id_podniku;
            if (akcia.id_obrazka != null) id_obrazka = (int) akcia.id_obrazka;
            if (akcia.text_id != null) text_id = (int) akcia.text_id;
            platnost_od = akcia.platnost_od;
            platnost_do = akcia.platnost_do;
            if (akcia.akciova_cena != null) akciova_cena = (int) akcia.akciova_cena;
            
            text = new BText(akcia.text);
            menu = new BMenu(akcia.menu);
            obrazok = new BObrazok(akcia.obrazok);
            
            entityAkcia = akcia;
        }

        
    }
}
