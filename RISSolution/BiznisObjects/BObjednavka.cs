using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;
using TransferObjects;

namespace BiznisObjects
{
    public class BObjednavka
    {
        public int id_objednavky { get; set; }
        public int id_stola { get; set; }
        public int id_uctu { get; set; }
        public DateTime? datum_objednania { get; set; }
        public int potvrdena { get; set; }
        public DateTime? datum_zaplatenia { get; set; }
        public double suma { get; set; }

        public BStol stol { get; set; }
        public ICollection<BObjednavka_menu> objednavka_menu { get; set; }
        public BUcet ucet { get; set; }

        public objednavka entityObjednavka;

        public BObjednavka(objednavka o)
        {
            id_objednavky = o.id_objednavky;
            id_stola = o.id_stola;
            id_uctu = o.id_uctu;
            datum_objednania = o.datum_objednania;
            if (o.datum_zaplatenia != null) datum_zaplatenia = (DateTime) o.datum_zaplatenia;
            if (o.potvrdena != null) potvrdena = (int) o.potvrdena;
            suma = o.suma;

            //stol = new BStol(o.stol);
            //ucet = new BUcet(o.ucet);

            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in o.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }
        }

        /// <summary>
        /// Vytvorí novú entitu objednavky podľa parametrov a uloží do databázy ak parameter risTabuľky nie je NULL
        /// </summary>
        /// <param name="id_stola">id stola</param>
        /// <param name="id_uctu">id uctu</param>
        /// <param name="suma">suma cien jedal v objednavke</param>
        /// <param name="risContext">kontext databázy</param>
        public BObjednavka(int id_stola, int id_uctu, double suma, risTabulky risContext)
        {
            entityObjednavka = new objednavka
            {
                id_stola = id_stola,
                id_uctu = id_uctu,
                suma = suma,
                datum_objednania = DateTime.Now
            };
            if (risContext != null)
            {
                risContext.objednavka.Add(entityObjednavka);
                risContext.SaveChanges();
            }
        }

        public BObjednavka()
        {
        }

        public class BObjednavkaCol : Dictionary<int, BObjednavka>
        {
            private risTabulky risContext;

            public BObjednavkaCol(risTabulky risContext)
            {
                this.risContext = risContext;
            }

            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.objednavka select a;
                    AddAllFromQuery(temp);

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool GetAllNotAccepted()
            {
                try
                {
                    var temp = from a in risContext.objednavka where a.potvrdena == null select a;
                    AddAllFromQuery(temp);

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private void AddAllFromQuery(IQueryable<objednavka> temp)
            {
                List<objednavka> tempList = temp.ToList();
                foreach (var a in tempList)
                {
                    this.Add(a.id_objednavky, new BObjednavka(a));
                }
            }

            public TObjednavka GetById(int id)
            {
                var temp = from a in risContext.objednavka where a.id_objednavky == id select a;
                var bObjednavka = new BObjednavka(temp.ToList()[0]);
                return bObjednavka.ToTransferObject();
            }

            public IList<TObjednavka> ToTransferList()
            {
                IList<TObjednavka> result = new List<TObjednavka>();
                foreach (var objednavka in this)
                {
                    TObjednavka objednavkaTemp = objednavka.Value.ToTransferObject();
                    result.Add(objednavkaTemp);
                }
                return result;
            }
        }

        public TObjednavka ToTransferObject()
        {
            IList<TObjednavkaMenu> polozky = new List<TObjednavkaMenu>();
            foreach (var bObjednavkaMenu in objednavka_menu)
            {
                polozky.Add(bObjednavkaMenu.ToTransferObject());
            }
            TObjednavka tObjednavka = new TObjednavka(id_objednavky, id_stola, id_uctu, potvrdena, suma, polozky);
            if (datum_objednania != null)
            {                
                tObjednavka.DatumObjednania = datum_objednania.Value.ToString();
            }
            if (datum_zaplatenia != null)
            {
                tObjednavka.DatumZaplatenia = datum_zaplatenia.Value.ToString();
            }
            return tObjednavka;
        }
    }
}
