using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_alergens
    {
        public int alergen_Id { get; set; }
        public int food_id { get; set; }

        public virtual alergen alergen { get; set; }
        public virtual food food { get; set; }
    }
}
