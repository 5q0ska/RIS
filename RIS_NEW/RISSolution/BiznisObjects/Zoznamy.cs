using System;
using System.Linq;
using DatabaseEntities;

namespace BiznisObjects

    
{
    public class Zoznamy
    {

        public static BFoodType dajTypJedla(int id, risTabulky risContext)
        {
            return new BFoodType(risContext.food_type.First(p => p.food_type_id == id));
        }

        /*
        public static BSurovina dajSurovinu(int id, risTabulky risContext)
        {
            return new BSurovina(risContext.surovina.First(p => p.id_surovina == id));
        }
        */ // ercisk

        public static BFood dajJedlo(int id, risTabulky risContext)
        {
            return new BFood(risContext.food.First(p => p.food_id == id));
        }

        public static BRisUser dajUcet(String login, risTabulky risContext)
        {
            return new BRisUser(risContext.ris_user.First(p => p.email==(login)));
        }

    }
}
