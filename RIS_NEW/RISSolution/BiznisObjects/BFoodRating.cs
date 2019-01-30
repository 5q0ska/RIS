using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BFoodRating
    {
        public int FoodRatingId { get; set; }
        public int? UserId { get; set; }
        public int StarsCount { get; set; }
        public string RatingComment { get; set; }

        public BRisUser User { get; set; }
        public ICollection<BFoodRatings> FoodRatings { get; set; }

        public food_rating entityFoodRating { get; set; }

        public BFoodRating()
        {
            this.Reset();
        }

        public BFoodRating(food_rating foodRating)
        {
            FoodRatingId = foodRating.food_rating_id;
            if (foodRating.user_id != null) UserId = (int)foodRating.user_id;
            StarsCount = foodRating.stars_count;
            RatingComment = foodRating.rating_comment;

            User = new BRisUser(foodRating.user);
            FoodRatings = new List<BFoodRatings>();

            foreach (var food_Ratings1 in foodRating.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_Ratings1);
                FoodRatings.Add(pom);
            }

            entityFoodRating = foodRating;
        }

        private void Reset()
        {
            FoodRatingId = 0;
            UserId = null;
            StarsCount = 0;
            RatingComment = "";

            User = new BRisUser();
            FoodRatings = new List<BFoodRatings>();

            entityFoodRating = new food_rating();
        }

        private void FillBObject()
        {
            FoodRatingId = entityFoodRating.food_rating_id;
            if (entityFoodRating.user_id != null) UserId = (int)entityFoodRating.user_id;
            StarsCount = entityFoodRating.stars_count;
            RatingComment = entityFoodRating.rating_comment;

            User = new BRisUser(entityFoodRating.user);
            FoodRatings = new List<BFoodRatings>();

            foreach (var food_Ratings1 in entityFoodRating.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_Ratings1);
                FoodRatings.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityFoodRating.food_rating_id = FoodRatingId;
            entityFoodRating.user_id = UserId;
            entityFoodRating.stars_count = StarsCount;
            entityFoodRating.rating_comment = RatingComment;
            entityFoodRating.user = User.entityRisUser;

            foreach (var food_Ratings1 in FoodRatings)
            {
                entityFoodRating.food_ratings.Add(food_Ratings1.entityFoodRatings);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodRatingId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_rating.Add(entityFoodRating);
                    risContext.SaveChanges();
                    FoodRatingId = entityFoodRating.food_rating_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_rating where a.food_rating_id == FoodRatingId select a;
                    entityFoodRating = temp.Single();
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
                var temp = risContext.food_rating.First(i => i.food_rating_id == FoodRatingId);
                risContext.food_rating.Remove(temp);
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
                var temp = from a in risContext.food_rating where a.food_rating_id == id select a;
                entityFoodRating = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodRatingCol : Dictionary<int, BFoodRating>
        {

            public BFoodRatingCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.food_rating select a;
                    List<food_rating> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_rating_id, new BFoodRating(a));
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
