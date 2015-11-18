using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BJedlo_surovina
    {
        public int id_surovina { get; set; }
        public int id_jedla { get; set; }
        public int id_typu { get; set; }
        public double mnozstvo { get; set; }

        public BJedlo jedlo { get; set; }
        public BSurovina surovina { get; set; }

        public jedlo_surovina entityJedloSurovina { get; set; }

        public BJedlo_surovina()
        {
            this.Reset();
        }

        public BJedlo_surovina(jedlo_surovina js)
        {
            id_surovina = js.id_surovina;
            id_jedla = js.id_jedla;
            id_typu = js.id_typu;
            mnozstvo = js.mnozstvo;
            
            jedlo = new BJedlo(js.jedlo);
            surovina = new BSurovina(js.surovina);

            entityJedloSurovina = js;
        }
        private void Reset()
        {
            id_surovina = 0;
            id_jedla = 0;
            id_typu = 0;
            mnozstvo = 0;

            jedlo = new BJedlo();
            surovina = new BSurovina(new surovina());

            entityJedloSurovina = new jedlo_surovina();
        }

        private void FillBObject()
        {
            id_surovina = entityJedloSurovina.id_surovina;
            id_jedla = entityJedloSurovina.id_jedla;
            id_typu = entityJedloSurovina.id_typu;
            mnozstvo = entityJedloSurovina.mnozstvo;

            jedlo = new BJedlo(entityJedloSurovina.jedlo);
            surovina = new BSurovina(entityJedloSurovina.surovina);
        }

        private void FillEntity()
        {
            entityJedloSurovina.id_surovina = id_surovina;
            entityJedloSurovina.id_jedla = id_jedla;
            entityJedloSurovina.id_typu = id_typu;
            entityJedloSurovina.mnozstvo = mnozstvo;

            entityJedloSurovina.jedlo = jedlo.entityJedlo;
            entityJedloSurovina.surovina = surovina.entitySurovina;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.jedlo_surovina where a.id_surovina == id_surovina &&
                               a.id_jedla == id_jedla select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.jedlo_surovina.Add(entityJedloSurovina);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityJedloSurovina = temp.Single();
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
                var temp = risContext.jedlo_surovina.First(i => i.id_surovina == id_surovina && i.id_jedla == id_jedla);
                risContext.jedlo_surovina.Remove(temp);
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

        public bool Get(risTabulky risContext, int id_jedla, int id_surovina)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.jedlo_surovina where a.id_surovina == id_surovina && 
                               a.id_jedla == id_jedla select a;
                entityJedloSurovina = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BJedlo_surovinaCol : Dictionary<string, BJedlo_surovina>
        {

            public BJedlo_surovinaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.jedlo_surovina select a;
                    List<jedlo_surovina> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_jedla + "," + a.id_surovina, new BJedlo_surovina(a));
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
