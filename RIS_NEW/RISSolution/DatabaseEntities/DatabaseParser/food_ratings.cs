using System;
using System.Collections.Generic;

namespace DatabaseEntities
{
    public partial class food_ratings
    {
        public int food_rating_id { get; set; }
        public int food_id { get; set; }

        public virtual food food { get; set; }
        public virtual food_rating food_rating { get; set; }
    }
}
