using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFood
    {
        public int FoodId { get; set; }
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public double PriceWithoutAdditions { get; set; }
        public int PreparationTime { get; set; }
        public double Weight { get; set; }
        public double? PriceWithAdditions { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public BFoodType FoodType { get; set; }
        public ICollection<BFoodAdditions> FoodAdditions { get; set; }
        public ICollection<BFoodAlergens> FoodAlergens { get; set; }
        public ICollection<BFoodRatings> FoodRatings { get; set; }
        public ICollection<BOrderFoods> OrderFoods { get; set; }

        public food entityFood { get; set; }

        public BFood()
        {
            this.Reset();
        }

        public BFood(food food)
        {
            FoodId = food.food_id;
            FoodTypeId = food.food_type_id;
            Name = food.name;
            PriceWithoutAdditions = food.price_without_additions;
            PreparationTime = food.preparation_time;
            Weight = food.weight;
            if (food.price_with_additions != null) PriceWithAdditions = food.price_with_additions;
            Description = food.description;
            Image = food.image;

            FoodType = new BFoodType(food.food_type);
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            foreach (var food_Additions1 in food.food_additions)
            {
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }

            foreach (var food_alergens1 in food.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_alergens1);
                FoodAlergens.Add(pom);
            }

            foreach (var food_ratings1 in food.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_ratings1);
                FoodRatings.Add(pom);
            }

            foreach (var order_foods1 in food.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_foods1);
                OrderFoods.Add(pom);
            }

            entityFood = food;
        }

        private void Reset()
        {
            FoodId = 0;
            FoodTypeId = 0;
            Name = "";
            PriceWithoutAdditions = 0;
            PreparationTime = 0;
            Weight = 0;
            PriceWithAdditions = null;
            Description = "";
            Image = new byte[65535];
            FoodType = new BFoodType();
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            entityFood = new food();
        }

        private void FillBObject()
        {
            FoodId = entityFood.food_id;
            FoodTypeId = entityFood.food_type_id;
            Name = entityFood.name;
            PriceWithoutAdditions = entityFood.price_without_additions;
            PreparationTime = entityFood.preparation_time;
            Weight = entityFood.weight;
            if (entityFood.price_with_additions != null) PriceWithAdditions = entityFood.price_with_additions;
            Description = entityFood.description;
            Image = entityFood.image;

            FoodType = new BFoodType(entityFood.food_type);
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            foreach (var food_Additions1 in entityFood.food_additions)
            {
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }

            foreach (var food_alergens1 in entityFood.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_alergens1);
                FoodAlergens.Add(pom);
            }

            foreach (var food_ratings1 in entityFood.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_ratings1);
                FoodRatings.Add(pom);
            }

            foreach (var order_foods1 in entityFood.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_foods1);
                OrderFoods.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityFood.food_id = FoodId;
            entityFood.food_type_id = FoodTypeId;
            entityFood.name = Name;
            entityFood.price_without_additions = PriceWithoutAdditions;
            entityFood.preparation_time = PreparationTime;
            entityFood.weight = Weight;
            entityFood.price_with_additions = PriceWithAdditions;
            entityFood.description = Description;
            entityFood.image = Image;

            entityFood.food_type = FoodType.entityFoodType;

            foreach (var food_Additions1 in FoodAdditions)
            {
                entityFood.food_additions.Add(food_Additions1.entityFoodAdditions);
            }

            foreach (var food_alergens1 in FoodAlergens)
            {
                entityFood.food_alergens.Add(food_alergens1.entityFoodAlergens);
            }

            foreach (var food_ratings1 in FoodRatings)
            {
                entityFood.food_ratings.Add(food_ratings1.entityFoodRatings);
            }

            foreach (var order_foods1 in OrderFoods)
            {
                entityFood.order_foods.Add(order_foods1.entityOrderFoods);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food.Add(entityFood);
                    risContext.SaveChanges();
                    FoodId = entityFood.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food where a.food_id == FoodId select a;
                    entityFood = temp.Single();
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
                var temp = risContext.food.First(i => i.food_id == FoodId);
                risContext.food.Remove(temp);
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
                var temp = from a in risContext.food where a.food_id == id select a;
                entityFood = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodCol : Dictionary<int, BFood>
        {

            public BFoodCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food select a;
                    List<food> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFood(a));
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
