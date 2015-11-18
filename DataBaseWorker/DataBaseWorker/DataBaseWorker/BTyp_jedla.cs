using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BTyp_jedla
    {
        public int id_typu { get; set; }
        public int typ { get; set; }

        public ICollection<BJedlo> jedlo { get; set; }
        public BText text { get; set; }

        public typ_jedla entityTypJedla { get; set; }

        public BTyp_jedla(typ_jedla tj)
        {
            id_typu = tj.id_typu;
            typ = tj.typ;
            text = new BText(tj.text);

            jedlo = new List<BJedlo>();
            foreach (var jedlo1 in tj.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }
            entityTypJedla = tj;
        }

        public BTyp_jedla()
        {
            this.Reset();
        }

        private void Reset()
        {
            id_typu = 0;
            typ = 0;
            text = new BText();

            jedlo = new List<BJedlo>();
            entityTypJedla = new typ_jedla();
        }

        private void FillBObject()
        {
            id_typu = entityTypJedla.id_typu;
            typ = entityTypJedla.typ;
            text = new BText(entityTypJedla.text);

            jedlo = new List<BJedlo>();
            foreach (var jedlo1 in entityTypJedla.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }
        }

        private void FillEntity()
        {
            id_typu = id_typu;
            entityTypJedla.typ = typ;
            entityTypJedla.text = text.entityText;

            foreach (var jedlo1 in jedlo)
            {
                entityTypJedla.jedlo.Add(jedlo1.entityJedlo);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_typu == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.typ_jedla.Add(entityTypJedla);
                    risContext.SaveChanges();
                    id_typu = entityTypJedla.id_typu;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.typ_jedla where a.id_typu == id_typu select a;
                    entityTypJedla = temp.Single();
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
                var temp = risContext.typ_jedla.First(i => i.id_typu == id_typu);
                risContext.typ_jedla.Remove(temp);
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

        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.typ_jedla where a.id_typu == id select a;
                entityTypJedla = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTyp_jedlaCol : Dictionary<int, BTyp_jedla>
        {

            public BTyp_jedlaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.typ_jedla select a;
                    List<typ_jedla> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_typu, new BTyp_jedla(a));
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
