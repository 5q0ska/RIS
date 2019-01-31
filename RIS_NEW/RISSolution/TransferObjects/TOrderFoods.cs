using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TransferObjects
{
    [DataContract]
    public class TOrderFoods
    {
        [DataMember]
        public int? id { get; set; }

        [DataMember]
        public int FoodId { get; set; }

        [DataMember]
        public int OrderId { get; set; }


        public TOrderFoods(int id, int FoodId, int OrderId)
        {
            this.id = id;
            this.FoodId = FoodId;
            this.OrderId = OrderId;
        }

        public TOrderFoods(int FoodId, int OrderId)
        {
            this.id = null;
            this.FoodId = FoodId;
            this.OrderId = OrderId;
        }

        public TOrderFoods(int id)
        {
            this.id = id;
            this.FoodId = FoodId;
            this.OrderId = OrderId;
        }
    }
}
