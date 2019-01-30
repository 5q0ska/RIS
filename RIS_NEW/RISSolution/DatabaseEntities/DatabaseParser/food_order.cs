using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_order
    {
        public food_order()
        {
            order_foods = new HashSet<order_foods>();
            user_orders = new HashSet<user_orders>();
        }

        public int food_order_id { get; set; }
        public DateTimeOffset order_date { get; set; }
        public double total_price { get; set; }
        public int is_paid { get; set; }
        public int is_sended { get; set; }
        public double? discount_price { get; set; }

        public virtual ICollection<order_foods> order_foods { get; set; }
        public virtual ICollection<user_orders> user_orders { get; set; }
    }
}
