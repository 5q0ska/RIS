using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_additions
    {
        public int addition_id { get; set; }
        public int food_id { get; set; }

        public virtual addition addition { get; set; }
        public virtual food food { get; set; }
    }
}
