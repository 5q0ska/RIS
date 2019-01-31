using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TFood : TransferEntity
    {
        private ICollection<TAddition> list_food_additions;
        private ICollection<TAlergen> list_food_alergens;

        [DataMember]
        public int FoodId { get; set; }

        [DataMember]
        public int FoodTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double PriceWithoutAdditions { get; set; }

        [DataMember]
        public int PreparationTime { get; set; }

        [DataMember]
        public double Weight { get; set; }

        [DataMember]
        public double? PriceWithAdditions { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public ICollection<TAddition> FoodAdditions
        {
            get { return list_food_additions; }
            set { list_food_additions = value; }
        }

        [DataMember]
        public ICollection<TAlergen> FoodAlergens
        {
            get { return list_food_alergens; }
            set { list_food_alergens = value; }
        }


        public TFood(int FoodId, int FoodTypeId, string Name, double PriceWithoutAdditions, int PreparationTime, double Weight, double? PriceWithAdditions, string Description, byte[] Image)
        {
            this.FoodId = FoodId;
            this.FoodTypeId = FoodTypeId;
            this.Name = Name;
            this.PriceWithoutAdditions = PriceWithoutAdditions;
            this.PreparationTime = PreparationTime;
            this.Weight = Weight;
            this.PriceWithAdditions = PriceWithAdditions;
            this.Description = Description;
            this.Image = Image;

            this.list_food_additions = new List<TAddition>();
            this.list_food_alergens = new List<TAlergen>();
        }

        public TFood(int FoodId, int FoodTypeId, string Name, double PriceWithoutAdditions, int PreparationTime, double Weight, double? PriceWithAdditions, string Description, byte[] Image, TFoodType foodType, ICollection<TAddition> foodAdditions, ICollection<TAlergen> foodAlergens)
        {
            this.FoodId = FoodId;
            this.FoodTypeId = FoodTypeId;
            this.Name = Name;
            this.PriceWithoutAdditions = PriceWithoutAdditions;
            this.PreparationTime = PreparationTime;
            this.Weight = Weight;
            this.PriceWithAdditions = PriceWithAdditions;
            this.Description = Description;
            this.Image = Image;

            this.list_food_additions = foodAdditions;
            this.list_food_alergens = foodAlergens;
        }

        public TFood(int FoodId)
        {
            this.FoodId = FoodId;
            this.FoodTypeId = 0;
            this.Name = "";
            this.PriceWithoutAdditions = 0;
            this.PreparationTime = 0;
            this.Weight = 0;
            this.PriceWithAdditions = null;
            this.Description = "";
            this.Image = new byte[65535];

            this.list_food_additions = new List<TAddition>();
            this.list_food_alergens = new List<TAlergen>();
        }

        public void PridajPrilohu(TAddition priloha)
        {
            list_food_additions.Add(priloha);
        }

        public void PridajAlergen(TAlergen alergen)
        {
            list_food_alergens.Add(alergen);
        }
    }
}
