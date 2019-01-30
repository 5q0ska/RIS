using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFoodType
    {
        public int FoodTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<BFood> Food { get; set; }

        public food_type entityFoodType { get; set; }

        public BFoodType()
        {
            this.Reset();
        }

        public BFoodType(food_type foodType)
        {
            FoodTypeId = foodType.food_type_id;
            Name = foodType.name;

            Food = new List<BFood>();
            foreach (var food1 in foodType.food)
            {
                BFood pom = new BFood(food1);
                Food.Add(pom);
            }

            entityFoodType = foodType;
        }

        private void Reset()
        {
            FoodTypeId = 0;
            Name = "";
            Food = new List<BFood>();

            entityFoodType = new food_type();
        }

        private void FillBObject()
        {
            FoodTypeId = entityFoodType.food_type_id;
            Name = entityFoodType.name;

            Food = new List<BFood>();
            foreach (var food1 in entityFoodType.food)
            {
                BFood pom = new BFood(food1);
                Food.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityFoodType.food_type_id = FoodTypeId;
            entityFoodType.name = Name;

            foreach (var food1 in Food)
            {
                entityFoodType.food.Add(food1.entityFood);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodTypeId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_type.Add(entityFoodType);
                    risContext.SaveChanges();
                    FoodTypeId = entityFoodType.food_type_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_type where a.food_type_id == FoodTypeId select a;
                    entityFoodType = temp.Single();
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
                var temp = risContext.food_type.First(i => i.food_type_id == FoodTypeId);
                risContext.food_type.Remove(temp);
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
                var temp = from a in risContext.food_type where a.food_type_id == id select a;
                entityFoodType = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodTypeCol : Dictionary<int, BFoodType>
        {

            public BFoodTypeCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food_type select a;
                    List<food_type> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_type_id, new BFoodType(a));
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
