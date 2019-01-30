using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_type
    {
        public food_type()
        {
            food = new HashSet<food>();
        }

        public int food_type_id { get; set; }
        public string name { get; set; }

        public virtual ICollection<food> food { get; set; }
    }
}
