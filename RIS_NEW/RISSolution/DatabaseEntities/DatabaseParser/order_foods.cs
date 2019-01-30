using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class order_foods
    {
        public int food_id { get; set; }
        public int order_id { get; set; }

        public virtual food food { get; set; }
        public virtual food_order order { get; set; }
    }
}
