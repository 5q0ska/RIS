using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class table_reservations
    {
        public string table_id { get; set; }
        public int user_id { get; set; }
        public DateTimeOffset date_time { get; set; }

        public virtual table table { get; set; }
        public virtual ris_user user { get; set; }
    }
}
