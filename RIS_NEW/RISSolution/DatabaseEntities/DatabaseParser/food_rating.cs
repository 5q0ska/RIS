using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_rating
    {
        public food_rating()
        {
            food_ratings = new HashSet<food_ratings>();
        }

        public int food_rating_id { get; set; }
        public int? user_id { get; set; }
        public int stars_count { get; set; }
        public string rating_comment { get; set; }

        public virtual ris_user user { get; set; }
        public virtual ICollection<food_ratings> food_ratings { get; set; }
    }
}
