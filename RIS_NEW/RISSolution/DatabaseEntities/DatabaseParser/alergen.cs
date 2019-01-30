using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class alergen
    {
        public alergen()
        {
            food_alergens = new HashSet<food_alergens>();
        }

        public int alergen_id { get; set; }
        public string description { get; set; }

        public virtual ICollection<food_alergens> food_alergens { get; set; }
    }
}
