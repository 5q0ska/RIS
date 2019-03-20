using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class dining_table
    {
        public dining_table()
        {
            table_reservations = new HashSet<table_reservations>();
        }

        public string table_id { get; set; }

        public virtual ICollection<table_reservations> table_reservations { get; set; }
    }
}
