using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;
using DataBaseWorker;

namespace Klient
{
    class Program
    {
        static void Main(string[] args)
        {
            risTabulky risContext = new risTabulky();
            BTyp_jedla.BTypJedlaCol typyJedla = new BTyp_jedla.BTypJedlaCol(risContext);
            typyJedla.GetAll();
            
            foreach (var typ in typyJedla)
            {
                Console.WriteLine(typ.Value.text.getPreklad("sk"));
            }
            Console.ReadLine();
            int a = 0;
            /*  BManager BM = new BManager();
            List<BJedlo> risContext = BM.jedloList;*/

            //stol stol = new stol
            //{
            //    id_stola = 1,
            //    pocet_miest = 6
            //};
            ////insert
            //risContext.stol.Add(stol);

            //risContext.SaveChanges();

            ////update

            //try
            //{
            //    stol stolUpdate = new stol
            //    {
            //        id_stola = 8,
            //        pocet_miest = 10

            //    };
            //    risContext.stol.Attach(stolUpdate);

            //    var entry = risContext.Entry(stolUpdate);
            //    entry.State = EntityState.Modified;

            //    entry.Property(e => e.pocet_miest).IsModified = true;

            //    risContext.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.InnerException.ToString());
            //}


            ////delete
            //stol stolDelete = risContext.stol.First(i => i.id_stola == stol.id_stola);
            //risContext.stol.Remove(stolDelete);
            //risContext.SaveChanges();
        }
    }
}
