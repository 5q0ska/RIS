using System;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TFoodType
    {
        public TFoodType(int FoodTypeId, string Name)
        {
            this.FoodTypeId = FoodTypeId;
            this.Name = Name;
        }

        public TFoodType()
        {

        }

        [DataMember]
        public int FoodTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
