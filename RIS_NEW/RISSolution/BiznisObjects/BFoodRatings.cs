using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFoodRatings
    {
        public int FoodRatingId { get; set; }
        public int FoodId { get; set; }

        public BFood Food { get; set; }
        public BFoodRating FoodRating { get; set; }

        public food_ratings entityFoodRatings { get; set; }

        public BFoodRatings()
        {
            this.Reset();
        }

        public BFoodRatings(food_ratings foodRatings)
        {
            FoodRatingId = foodRatings.food_rating_id;
            FoodId = foodRatings.food_id;

            FoodRating = new BFoodRating(foodRatings.food_rating);
            Food = new BFood(foodRatings.food);

            entityFoodRatings = foodRatings;
        }

        private void Reset()
        {
            FoodRatingId = 0;
            FoodId = 0;

            FoodRating = new BFoodRating();
            Food = new BFood();

            entityFoodRatings = new food_ratings();
        }

        private void FillBObject()
        {
            FoodRatingId = entityFoodRatings.food_rating_id;
            FoodId = entityFoodRatings.food_id;

            FoodRating = new BFoodRating(entityFoodRatings.food_rating);
            Food = new BFood(entityFoodRatings.food);
        }

        private void FillEntity()
        {
            entityFoodRatings.food_rating_id = FoodRatingId;
            entityFoodRatings.food_id = FoodId;
            entityFoodRatings.food_rating = FoodRating.entityFoodRating;
            entityFoodRatings.food = Food.entityFood;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_ratings.Add(entityFoodRatings);
                    risContext.SaveChanges();
                    FoodId = entityFoodRatings.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_ratings where a.food_id == FoodId && a.food_rating_id == FoodRatingId select a;
                    entityFoodRatings = temp.Single();
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
                var temp = risContext.food_ratings.First(i => i.food_id == FoodId && i.food_rating_id == FoodRatingId);
                risContext.food_ratings.Remove(temp);
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
                var temp = from a in risContext.food_ratings where a.food_id == id[1] && a.food_rating_id == id[0] select a;
                entityFoodRatings = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodRatingsCol : Dictionary<int, BFoodRatings>
        {

            public BFoodRatingsCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food_ratings select a;
                    List<food_ratings> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFoodRatings(a));
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
