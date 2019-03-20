using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food
    {
        public food()
        {
            food_additions = new HashSet<food_additions>();
            food_alergens = new HashSet<food_alergens>();
            food_ratings = new HashSet<food_ratings>();
            order_foods = new HashSet<order_foods>();
        }

        public int food_id { get; set; }
        public int food_type_id { get; set; }
        public string name { get; set; }
        public double price_without_additions { get; set; }
        public int preparation_time { get; set; }
        public double weight { get; set; }
        public double? price_with_additions { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }

        public virtual food_type food_type { get; set; }
        public virtual ICollection<food_additions> food_additions { get; set; }
        public virtual ICollection<food_alergens> food_alergens { get; set; }
        public virtual ICollection<food_ratings> food_ratings { get; set; }
        public virtual ICollection<order_foods> order_foods { get; set; }
    }
}
