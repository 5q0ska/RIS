using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BAddition
    {
        public int AdditionId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public ICollection<BFoodAdditions> FoodAdditions { get; set; }

        public addition entityAddition { get; set; }

        public BAddition()
        {
            this.Reset();
        }

        public BAddition(addition addition)
        {
            AdditionId = addition.addition_id;
            Name = addition.name;
            Weight = addition.weight;
            Price = addition.price;
            Description = addition.description;

            FoodAdditions = new List<BFoodAdditions>();
            foreach (var food_Additions1 in addition.food_additions) { 
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }

            entityAddition = addition;
        }

        private void Reset()
        {
            AdditionId = 0;
            Name = "";
            Weight = 0;
            Price = 0;
            Description = "";
            FoodAdditions = new List<BFoodAdditions>();

            entityAddition = new addition();
        }

        private void FillBObject()
        {
            AdditionId = entityAddition.addition_id;
            Name = entityAddition.name;
            Weight = entityAddition.weight;
            Price = entityAddition.price;
            Description = entityAddition.description;

            FoodAdditions = new List<BFoodAdditions>();
            foreach (var food_Additions1 in entityAddition.food_additions)
            {
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityAddition.addition_id = AdditionId;
            entityAddition.name = Name;
            entityAddition.weight = Weight;
            entityAddition.price = Price;
            entityAddition.description = Description;

            foreach (var food_Additions1 in FoodAdditions)
            {
                entityAddition.food_additions.Add(food_Additions1.entityFoodAdditions);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (AdditionId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.addition.Add(entityAddition);
                    risContext.SaveChanges();
                    AdditionId = entityAddition.addition_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.addition where a.addition_id == AdditionId select a;
                    entityAddition = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    this.FillBObject();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.addition.First(i => i.addition_id == AdditionId);
                risContext.addition.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
                this.Reset();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.addition where a.addition_id == id select a;
                entityAddition = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BAdditionCol : Dictionary<int, BAddition>
        {

            public BAdditionCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.addition select a;
                    List<addition> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.addition_id, new BAddition(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
