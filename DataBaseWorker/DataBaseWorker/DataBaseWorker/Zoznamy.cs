using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker

    
{
    /// <summary>
    ///   Reprezentuje zoznamy
    /// </summary>
    public class Zoznamy
    {
        /// <summary>
        ///   Vrácia typ_jedla z databázy s daným identifikátorm
        /// </summary>
        /// <param name="id">identifikátor typu jedla</param>
        /// <param name="risContext">kontext databázy</param>
        /// <returns>jedlo</returns>
        public static BTyp_jedla dajTypJedla(int id, risTabulky risContext)
        {
            typ_jedla typ=risContext.typ_jedla.First(p => p.id_typu == id));
            if (typ != null)
            {
                return new BTyp_jedla(typ);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///    Vrácia surovinu z databázy s daným identifikátorm
        /// </summary>
        /// <param name="id">identifikátor suroviny</param>
        /// <param name="risContext">kontext databázy</param>
        /// <returns></returns>
        public static BSurovina dajSurovinu(int id, risTabulky risContext)
        {
            surovina sur = risContext.surovina.First(p => p.id_surovina == id);
            if (sur != null)
            {
                return new BSurovina(sur);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///   Vrácia jedlo z databázy s daným identifikátorom
        /// </summary>
        /// <param name="id">identifikátor jedla</param>
        /// <param name="risContext">kontext databázy</param>
        /// <returns></returns>
        public static BJedlo dajJedlo(int id, risTabulky risContext)
        {
            jedlo res = risContext.jedlo.First(p => p.id_jedla == id);
            if (res != null)
            {
                return new BJedlo(res);
            } else
            {
                return null;
            }
        }

        /// <summary>
        ///   Vrácia účet s daným loginom
        /// </summary>
        /// <param name="login">prihlasovací login</param>
        /// <param name="risContext">kontext databázy</param>
        /// <returns>ucet s danym loginom</returns>
        public static BUcet dajUcet(String login, risTabulky risContext)
        {
            ucet uct = risContext.ucet.First(p => p.login == (login));
            if (uct != null)
            {
                return new BUcet(uct);
            }
            else
            {
                return null;
            }
        }
    }
}
