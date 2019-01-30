using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFoodAdditions
    {
        public int AdditionId { get; set; }
        public int FoodId { get; set; }

        public BAddition Addition { get; set; }
        public BFood Food { get; set; }

        public food_additions entityFoodAdditions { get; set; }

        public BFoodAdditions()
        {
            this.Reset();
        }

        public BFoodAdditions(food_additions foodAdditions)
        {
            AdditionId = foodAdditions.addition_id;
            FoodId = foodAdditions.food_id;

            Addition = new BAddition(foodAdditions.addition);
            Food = new BFood(foodAdditions.food);

            entityFoodAdditions = foodAdditions;
        }

        private void Reset()
        {
            AdditionId = 0;
            FoodId = 0;

            Addition = new BAddition();
            Food = new BFood();

            entityFoodAdditions = new food_additions();
        }

        private void FillBObject()
        {
            AdditionId = entityFoodAdditions.addition_id;
            FoodId = entityFoodAdditions.food_id;

            Addition = new BAddition(entityFoodAdditions.addition);
            Food = new BFood(entityFoodAdditions.food);
        }

        private void FillEntity()
        {
            entityFoodAdditions.addition_id = AdditionId;
            entityFoodAdditions.food_id = FoodId;
            entityFoodAdditions.addition = Addition.entityAddition;
            entityFoodAdditions.food = Food.entityFood;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_additions.Add(entityFoodAdditions);
                    risContext.SaveChanges();
                    FoodId = entityFoodAdditions.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_additions where a.food_id == FoodId && a.addition_id == AdditionId select a;
                    entityFoodAdditions = temp.Single();
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
                var temp = risContext.food_additions.First(i => i.food_id == FoodId && i.addition_id == AdditionId);
                risContext.food_additions.Remove(temp);
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

        public bool Get(risTabulky risContext, int[] id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.food_additions where a.food_id == id[1] && a.addition_id == id[0] select a;
                entityFoodAdditions = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodAdditionsCol : Dictionary<int, BFoodAdditions>
        {

            public BFoodAdditionsCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food_additions select a;
                    List<food_additions> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFoodAdditions(a));
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
