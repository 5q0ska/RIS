using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class table_reservations
    {
        public string table_id { get; set; }
        public int user_id { get; set; }
        public DateTime date_time { get; set; }

        public virtual dining_table dining_table { get; set; }
        public virtual ris_user user { get; set; }
    }
}
