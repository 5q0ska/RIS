using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BOrderFoods
    {
        public int FoodId { get; set; }
        public int OrderId { get; set; }

        public BFood Food { get; set; }
        public BFoodOrder Order { get; set; }

        public order_foods entityOrderFoods { get; set; }

        public BOrderFoods()
        {
            this.Reset();
        }

        public BOrderFoods(order_foods orderFoods)
        {
            FoodId = orderFoods.food_id;
            OrderId = orderFoods.order_id;

            Food = new BFood(orderFoods.food);
            Order = new BFoodOrder(orderFoods.order);

            entityOrderFoods = orderFoods;
        }

        private void Reset()
        {
            FoodId = 0;
            OrderId = 0;

            Food = new BFood();
            Order = new BFoodOrder();

            entityOrderFoods = new order_foods();
        }

        private void FillBObject()
        {
            FoodId = entityOrderFoods.food_id;
            OrderId = entityOrderFoods.order_id;

            Food = new BFood(entityOrderFoods.food);
            Order = new BFoodOrder(entityOrderFoods.order);
        }

        private void FillEntity()
        {
            entityOrderFoods.food_id = FoodId;
            entityOrderFoods.order_id = OrderId;
            entityOrderFoods.food = Food.entityFood;
            entityOrderFoods.order = Order.entityFoodOrder;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.order_foods.Add(entityOrderFoods);
                    risContext.SaveChanges();
                    FoodId = entityOrderFoods.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.order_foods where a.food_id == FoodId && a.order_id == OrderId select a;
                    entityOrderFoods = temp.Single();
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
                var temp = risContext.order_foods.First(i => i.food_id == FoodId && i.order_id == OrderId);
                risContext.order_foods.Remove(temp);
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
                var temp = from a in risContext.order_foods where a.food_id == id[0] && a.order_id == id[1] select a;
                entityOrderFoods = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BOrderFoodsCol : Dictionary<int, BOrderFoods>
        {

            public BOrderFoodsCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.order_foods select a;
                    List<order_foods> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BOrderFoods(a));
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
