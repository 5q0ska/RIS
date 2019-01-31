using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TFoodOrder
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int FoodOrderId { get; set; }

        [DataMember]
        public DateTimeOffset OrderDate { get; set; }

        [DataMember]
        public double TotalPrice { get; set; }

        [DataMember]
        public int IsPaid { get; set; }

        [DataMember]
        public int IsSended { get; set; }

        [DataMember]
        public double? DiscountPrice { get; set; }

        public TFoodOrder(int FoodOrderId, DateTimeOffset OrderDate, double TotalPrice, int IsPaid, int IsSended, double? DiscountPrice)
        {
            this.FoodOrderId = FoodOrderId;
            this.OrderDate = OrderDate;
            this.TotalPrice = TotalPrice;
            this.IsPaid = IsPaid;
            this.IsSended = IsSended;
            this.DiscountPrice = DiscountPrice;
        }
    }
}
