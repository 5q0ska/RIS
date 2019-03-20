using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class user_orders
    {
        public int order_id { get; set; }
        public int user_id { get; set; }

        public virtual food_order order { get; set; }
        public virtual ris_user user { get; set; }
    }
}
