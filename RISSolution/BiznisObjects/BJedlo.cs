using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;
using TransferObjects;


namespace BiznisObjects
{
    /// <summary>
    /// Reprezentuje jedlo 
    /// </summary>
    public class BJedlo : TransferTemplate
    {
        /// <summary>
        /// Unikátny identifikátor jedla v ponuke
        /// </summary>
        public int ID
        {
            get { return entity.id_jedla; }
            set { entity.id_jedla = value; }
        }

        /// <summary>
        /// Názov jedla
        /// </summary>
        public BText nazov
        {
            get { return new BText(entity.text); }
            set { entity.text = value.entityText; }
        }

        /// <summary>
        /// Množstvo kalorií v jedle
        /// </summary>
        public int? mnozstvo_kalorii
        {
            get { return entity.mnozstvo_kalorii; }
            set { entity.mnozstvo_kalorii = value; }
        }

        /// <summary>
        /// Typ jedla (hlavné, príloha...)
        /// </summary>
        public BTyp_jedla typ_jedla
        {
            get { return new BTyp_jedla(entity.typ_jedla); }
            set { entity.typ_jedla = value.entityTypJedla; }
        }

        /// <summary>
        /// Dĺžka prípravy jedla v sekundách
        /// </summary>
        public int? dlzka_pripravy
        {
            get { return entity.dlzka_pripravy; }
            set { entity.dlzka_pripravy = value; }
        }

        public BObrazok obrazok
        {
            get { return new BObrazok(entity.obrazok); }
            set { entity.obrazok = value.entityObrazok; }
        }

        /// <summary>
        /// Položky menu ,v ktorých sa nachádza dané jedlo
        /// </summary>
        public ICollection<BMenu> menu_obsahujuce_jedlo
        {
            get
            {
                ICollection<BMenu> mena = new List<BMenu>();
                foreach (var bmenu in menu_jedlo)
                {
                    mena.Add(bmenu.menu);
                }
                return mena;
            }
        }

        /// <summary>
        /// Suroviny ,ktoré obsahuje jedlo
        /// </summary>
        public ICollection<BSurovinaJedla> suroviny_jedla
        {
            get
            {
                ICollection<BSurovinaJedla> surovina = new List<BSurovinaJedla>();
                foreach (var surovinaJedla in jedlo_surovina)
                {
                    surovina.Add(surovinaJedla);
                }
                return surovina;
            }
        }


        private ICollection<BMenu_jedlo> menu_jedlo
        {
            get
            {
                List<BMenu_jedlo> menu_jedlo_temp = new List<BMenu_jedlo>();
                foreach (var menuJedlo in entity.menu_jedlo)
                {
                    BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                    menu_jedlo_temp.Add(pom);
                }
                return menu_jedlo_temp;
            }
            set { this.menu_jedlo = value; }
        }

        private ICollection<BSurovinaJedla> jedlo_surovina
        {
            get
            {
                List<BSurovinaJedla> jedlo_surovina_temp = new List<BSurovinaJedla>();
                foreach (var jedloSurovina in entity.jedlo_surovina)
                {
                    BSurovinaJedla pom = new BSurovinaJedla(jedloSurovina);
                    jedlo_surovina_temp.Add(pom);
                }
                return jedlo_surovina_temp;
            }
            set { this.jedlo_surovina = value; }
        }

        /// <summary>
        ///    Podrobný zoznam všetkých surovin jedla ako prenosová entita
        /// </summary>
        /// <param name="id_jazyk"></param>
        /// <returns>zoznám surovin jedla</returns>
        public IList<TSurovina> PE_suroviny_jedla(String id_jazyk)
        {
            List<TSurovina> result = new List<TSurovina>();

            foreach (var surovinaJedla in suroviny_jedla)
            {
                TransferEntity konkretnaSurovina = surovinaJedla.surovina.toTransferObject(id_jazyk);
                if (konkretnaSurovina != null)
                {
                    TSurovina pridavana = (TSurovina) konkretnaSurovina;
                    pridavana.Mnozstvo = surovinaJedla.mnozstvo;
                    result.Add(pridavana);
                }

            }

            return result;
        }


        /// <summary>
        /// Databázova entita daného objektu
        /// </summary>
        public jedlo entity;

        /// <summary>
        /// Vytvorí nové jedlo
        /// </summary>
        public BJedlo()
        {
            this.Reset();
            entity = new jedlo();
        }

        /// <summary>
        /// Vytvorí nové jedlo podľa informácie z databázy
        /// </summary>
        /// <param name="j"></param>
        public BJedlo(jedlo j)
        {
            entity = j;
        }


       
        
        /// <summary>
        /// Vytvorí novú entitu jedla podľa parametrov a uloží do databázy ak parameter risTabuľky nie je NULL
        /// </summary>
        /// <param name="id_nazvu">id textu nazov jedla</param>
        /// <param name="id_typu">id typu jedla</param>
        /// <param name="mnozstvoKalorii">množstvo kalorii jedla</param>
        /// <param name="dlzka_pripravy">dĺžka prípravy jedla</param>
        /// <param name="risContext">kontext databázy</param>
        public BJedlo(int id_nazvu, int id_typu, int? mnozstvoKalorii, int? dlzka_pripravy, risTabulky risContext/*, int id_obrazka*/)
        {
            entity = new jedlo();
            entity.id_typu = id_typu;
            entity.nazov = id_nazvu;
            entity.mnozstvo_kalorii = mnozstvoKalorii;
            entity.dlzka_pripravy = dlzka_pripravy;
            if (risContext != null)
            {
                risContext.jedlo.Add(entity);
                risContext.SaveChanges();
            }
            //entity.id_obrazka = id_obrazka;

        }

        /// <summary>
        /// Vytvorí novú entitu jedla podľa parametrov a uloží do databázy ak parameter risTabuľky nie je NULL
        /// </summary>
        /// <param name="nazov">Názov jedla</param>
        /// <param name="typ_jedla">Typ jedla</param>
        /// <param name="mnostvoKalorii">Množstvo kalorii jedla</param>
        /// <param name="dlzka_pripravy">Dĺžka prípravy jedla</param>
        /// <param name="risContext"></param>
        public BJedlo(BText nazov, BTyp_jedla typ_jedla, int mnostvoKalorii, int dlzka_pripravy/*, BObrazok obrazok*/, risTabulky risContext)
        {
            entity = new jedlo();
            entity.id_typu = typ_jedla.id_typu;
            entity.nazov = nazov.text_id;
            entity.mnozstvo_kalorii = mnostvoKalorii;
            entity.dlzka_pripravy = dlzka_pripravy;
            if (risContext != null)
            {
                risContext.jedlo.Add(entity);
                risContext.SaveChanges();
            }
            //entity.id_obrazka = obrazok.id_obrazka;
        }

        /// <summary>
        ///  Pridá surovinu k jedlu
        /// </summary>
        /// <param name="surovina">pridavaná surovina</param>
        /// <param name="mnozstvo">množstvo pridavanej suroviny</param>
        public void PridajSurovinu(BSurovina surovina, double mnozstvo)
        {
            jedlo_surovina tempSurovina=new jedlo_surovina();
            tempSurovina.id_surovina = surovina.ID;
            tempSurovina.mnozstvo = mnozstvo;
            entity.jedlo_surovina.Add(tempSurovina);
        }

        /// <summary>
        /// Odoberie od jedla surovinu
        /// </summary>
        /// <param name="surovina">surovinu ktoru chceme odobrať</param>
        public void OdoberSurovinu(BSurovina surovina)
        {
            jedlo_surovina removeItem=entity.jedlo_surovina.First(p => p.id_surovina == surovina.ID);
            entity.jedlo_surovina.Remove(removeItem);
        }

        /// <summary>
        ///   Navráti surovinu jedla s daným ID
        /// </summary>
        /// <param name="id_suroviny">id suroviny</param>
        /// <returns></returns>
        public BSurovinaJedla DajSurovinuJedla(int id_suroviny)
        {
            jedlo_surovina tempJedloSurovina = entity.jedlo_surovina.First(p => p.id_surovina == id_suroviny);
            if (tempJedloSurovina != null)
            {
                return new BSurovinaJedla(tempJedloSurovina);
            }
            return null;

        }

        private void Reset()
        {
            entity = null;
        }

        /// <summary>
        /// Uloží zmeny do databázy
        /// </summary>
        /// <param name="risContext">kontext databázy</param>
        /// <returns></returns>
        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
             
              risContext.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        /// <summary>
        /// Vymaže dané jedlo z databázy
        /// </summary>
        /// <param name="risContext">kontext databázy</param>
        /// <returns></returns>
        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.jedlo.First(i => i.id_jedla == ID);
                risContext.jedlo.Remove(temp);
                risContext.SaveChanges();
                success = true;
                this.Reset();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        /// <summary>
        /// Zoznám jedal ako dictionary s id a jedlom
        /// </summary>
        public class BJedloCol : Dictionary<int, BJedlo>, TransferTemplateList
        {
            private risTabulky risContext;

            public BJedloCol(risTabulky risContext)
            {
                this.risContext = risContext;
            }

            /// <summary>
            /// Naplní zoznám jedal všetkými jedlami z databázy
            /// </summary>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.jedlo select a;
                    List<jedlo> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_jedla, new BJedlo(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// Naplní zoznam surovín všetkými jedlami v databáze ,ktorých názvy začínajú na text ,ktorý je parametrom
            /// </summary>
            /// <param name="startingString">text, na ktorý majú začínať nazvy , aspoň 3 písmena</param>
            /// <param name="risContext">kontext databázy</param>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetNameStartingWith(String startingString)
            {
                try
                {   
                    var temp = risContext.jedlo.Select(x => x.text.text_id);
                    var tempPreklady = from a in risContext.preklad where temp.Contains(a.text_id) && a.preklad1.StartsWith(startingString) select a.text_id;
                    var jedla = from a in risContext.jedlo where tempPreklady.Contains(a.text.text_id) select a;


                    List<jedlo> tempList = jedla.ToList();
                    this.Clear();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_jedla, new BJedlo(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IList<TransferEntity> toTransferList(string kod_jazyka)
            {
                IList<TransferEntity> result = new List<TransferEntity>();
                foreach (var jedlo in this)
                {
                    var metadata = "";
                    if (jedlo.Value.obrazok != null)
                    {
                        metadata = jedlo.Value.obrazok.metadata;
                    }
                    TJedlo jedloTemp = new TJedlo(jedlo.Value.ID, jedlo.Value.nazov.getPreklad(kod_jazyka),
                        jedlo.Value.typ_jedla.id_typu, jedlo.Value.typ_jedla.text.getPreklad(kod_jazyka), jedlo.Value.dlzka_pripravy,
                        jedlo.Value.mnozstvo_kalorii, metadata);
                    jedloTemp.LanguageCode = kod_jazyka;
                    result.Add(jedloTemp);
                }
                return result;
            }
        }

        public TransferEntity toTransferObject(string kod_jazyka)
        {
            TJedlo result = new TJedlo(ID, nazov.getPreklad(kod_jazyka), typ_jedla.id_typu, typ_jedla.text.getPreklad(kod_jazyka), dlzka_pripravy, mnozstvo_kalorii, obrazok.metadata);
            foreach (var surovina in suroviny_jedla)
            {
                TSurovina s = new TSurovina(surovina.surovina.ID, surovina.surovina.nazov.getPreklad(kod_jazyka),
                    surovina.surovina.alergen, surovina.surovina.jednotka);
                s.LanguageCode = kod_jazyka;
                result.PridajSurovinu(s);
            }
            result.LanguageCode = kod_jazyka;
            return result;
        }

        public void updatefromTransferObject(TransferEntity transferEntity,risTabulky risContext)
        {
            if (transferEntity.GetType() == typeof (TJedlo))
            {
                TJedlo jedlo = (TJedlo) transferEntity;
                if (jedlo.Id.HasValue)
                {
                    if (!jedlo.Length.Equals(dlzka_pripravy))
                    {
                        entity.dlzka_pripravy = jedlo.Length;
                    }

                    if (!jedlo.TypeId.Equals(typ_jedla.id_typu))
                    {
                        entity.id_typu = jedlo.TypeId;
                    }

                    if (!jedlo.AmountOfCalories.Equals(mnozstvo_kalorii))
                    {
                        entity.mnozstvo_kalorii = jedlo.AmountOfCalories;
                    }

                    foreach (var surovina in suroviny_jedla)
                    {
                        TSurovina tempSurovina=jedlo.RawMaterial.First(p => p.Id == surovina.id_surovina);
                        if (tempSurovina != null)
                        {
                            if (!surovina.mnozstvo.Equals(tempSurovina.Mnozstvo))
                            {
                                surovina.mnozstvo = tempSurovina.Mnozstvo;
                            }
                        }
                        else
                        {
                            entity.jedlo_surovina.Remove(surovina.entityJedloSurovina);
                        }
                        
                    }


                    foreach (var surovina in jedlo.RawMaterial)
                    {
                        jedlo_surovina temp_bsurovina = entity.jedlo_surovina.First(p => p.id_surovina == surovina.Id);
                        if (temp_bsurovina == null)
                        {
                            entity.jedlo_surovina.Add(risContext.jedlo_surovina.First(p => p.id_surovina == surovina.Id));
                        }
                    }


                    foreach (var preklad in jedlo.Translations)
                    {
                        preklad temp_preklad = entity.text.preklad.First(p => p.kod_jazyka.Equals(preklad.Key));
                        if (temp_preklad == null)
                        {
                            preklad prkl = new preklad();
                            prkl.kod_jazyka = preklad.Key;
                            prkl.preklad1 = preklad.Value;
                            prkl.text = nazov.entityText;

                            risContext.preklad.Add(prkl);
                            entity.text.preklad.Add(prkl);
                        }
                        else
                        {
                            temp_preklad.preklad1 = preklad.Value;
                        }
                    }

                    foreach (var preklad in nazov.preklad)
                    {
                        String temp_preklad = jedlo.Translations.Keys.First(p => p.Equals(preklad.kod_jazyka));
                        if (temp_preklad==null)
                        {
                            nazov.preklad.Remove(preklad);
                        }
                    }

                    risContext.SaveChanges();


                } else
                {
                    this.Reset();
                    entity = new jedlo();
                 
                    entity.mnozstvo_kalorii = jedlo.AmountOfCalories;
                    entity.dlzka_pripravy = jedlo.Length;

                    foreach (var suroviny in jedlo.RawMaterial)
                    {
                        surovina surovinaTemp = risContext.surovina.First(p => p.id_surovina == suroviny.Id);
                        if (surovinaTemp != null)
                        {
                            jedlo_surovina tempJedloSurovina=new jedlo_surovina();
                            tempJedloSurovina.jedlo = entity;
                            tempJedloSurovina.surovina = surovinaTemp;
                            tempJedloSurovina.mnozstvo = suroviny.Mnozstvo;
                            entity.jedlo_surovina.Add(tempJedloSurovina);
                        }
                    }



                }
            }
        }


    }
}
