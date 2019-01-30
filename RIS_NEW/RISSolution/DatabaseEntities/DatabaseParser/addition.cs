using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class addition
    {
        public addition()
        {
            food_additions = new HashSet<food_additions>();
        }

        public int addition_id { get; set; }
        public string name { get; set; }
        public double weight { get; set; }
        public double price { get; set; }
        public string description { get; set; }

        public virtual ICollection<food_additions> food_additions { get; set; }
    }
}
