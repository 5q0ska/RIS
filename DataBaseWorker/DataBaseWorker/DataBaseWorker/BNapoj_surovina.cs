using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BNapoj_surovina
    {
        public int id_surovina { get; set; }
        public int id_napoja { get; set; }
        public int mnozstvo { get; set; }

        public BNapoj napoj { get; set; }
        public BSurovina surovina { get; set; }

        public napoj_surovina entityNapojSurovina { get; set; }

        public BNapoj_surovina()
        {
            this.Reset();
        }

        public BNapoj_surovina(napoj_surovina ns)
        {
            id_surovina = ns.id_surovina;
            id_napoja = ns.id_napoja;
            mnozstvo = ns.mnozstvo;

            napoj = new BNapoj(ns.napoj);
            surovina = new BSurovina(ns.surovina);

            entityNapojSurovina = ns;
        }

        private void Reset()
        {
            id_surovina = 0;
            id_napoja = 0;
            mnozstvo = 0;

            napoj = new BNapoj();
            surovina = new BSurovina(new surovina());

            entityNapojSurovina = new napoj_surovina();
        }

        private void FillBObject()
        {
            id_surovina = entityNapojSurovina.id_surovina;
            id_napoja = entityNapojSurovina.id_napoja;
            mnozstvo = entityNapojSurovina.mnozstvo;

            napoj = new BNapoj(entityNapojSurovina.napoj);
            surovina = new BSurovina(entityNapojSurovina.surovina);
        }

        private void FillEntity()
        {
            entityNapojSurovina.id_surovina = id_surovina;
            entityNapojSurovina.id_napoja = id_napoja;
            entityNapojSurovina.mnozstvo = mnozstvo;

            entityNapojSurovina.napoj = napoj.entityNapoj;
            entityNapojSurovina.surovina = surovina.entitySurovina;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.napoj_surovina where a.id_napoja == id_napoja &&
                               a.id_surovina == id_surovina select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.napoj_surovina.Add(entityNapojSurovina);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityNapojSurovina = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.napoj_surovina.First(i => i.id_napoja == id_napoja && i.id_surovina == id_surovina);
                risContext.napoj_surovina.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, int idNapoja, int idSuroviny)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.napoj_surovina where a.id_napoja == idNapoja && 
                               idSuroviny == idSuroviny select a;
                entityNapojSurovina = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BNapoj_surovinaCol : Dictionary<string, BNapoj_surovina>
        {

            public BNapoj_surovinaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.napoj_surovina select a;
                    List<napoj_surovina> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_napoja + "," + a.id_surovina, new BNapoj_surovina(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
