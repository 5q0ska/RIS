using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFoodAlergens
    {
        public int AlergenId { get; set; }
        public int FoodId { get; set; }

        public BAlergen Alergen { get; set; }
        public BFood Food { get; set; }

        public food_alergens entityFoodAlergens { get; set; }

        public BFoodAlergens()
        {
            this.Reset();
        }

        public BFoodAlergens(food_alergens foodAlergens)
        {
            AlergenId = foodAlergens.alergen_Id;
            FoodId = foodAlergens.food_id;

            Alergen = new BAlergen(foodAlergens.alergen);
            Food = new BFood(foodAlergens.food);

            entityFoodAlergens = foodAlergens;
        }

        private void Reset()
        {
            AlergenId = 0;
            FoodId = 0;

            Alergen = new BAlergen();
            Food = new BFood();

            entityFoodAlergens = new food_alergens();
        }

        private void FillBObject()
        {
            AlergenId = entityFoodAlergens.alergen_Id;
            FoodId = entityFoodAlergens.food_id;

            Alergen = new BAlergen(entityFoodAlergens.alergen);
            Food = new BFood(entityFoodAlergens.food);
        }

        private void FillEntity()
        {
            entityFoodAlergens.alergen_Id = AlergenId;
            entityFoodAlergens.food_id = FoodId;
            entityFoodAlergens.alergen = Alergen.entityAlergen;
            entityFoodAlergens.food = Food.entityFood;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_alergens.Add(entityFoodAlergens);
                    risContext.SaveChanges();
                    FoodId = entityFoodAlergens.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_alergens where a.food_id == FoodId && a.alergen_Id == AlergenId select a;
                    entityFoodAlergens = temp.Single();
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
                var temp = risContext.food_alergens.First(i => i.food_id == FoodId && i.alergen_Id == AlergenId);
                risContext.food_alergens.Remove(temp);
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
                var temp = from a in risContext.food_alergens where a.food_id == id[1] && a.alergen_Id == id[0] select a;
                entityFoodAlergens = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodAlergensCol : Dictionary<int, BFoodAlergens>
        {

            public BFoodAlergensCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food_alergens select a;
                    List<food_alergens> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFoodAlergens(a));
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
