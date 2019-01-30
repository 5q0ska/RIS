using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class ris_user
    {
        public ris_user()
        {
            food_rating = new HashSet<food_rating>();
            table_reservations = new HashSet<table_reservations>();
            user_orders = new HashSet<user_orders>();
        }

        public int ris_user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public byte[] image { get; set; }
        public double? discount_price { get; set; }
        public int? actual_order_id { get; set; }

        public virtual ICollection<food_rating> food_rating { get; set; }
        public virtual ICollection<table_reservations> table_reservations { get; set; }
        public virtual ICollection<user_orders> user_orders { get; set; }
    }
}
