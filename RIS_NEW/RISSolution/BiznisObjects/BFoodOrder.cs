using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;
using TransferObjects;

namespace BiznisObjects
{

    public class BFoodOrder
    {
        public int FoodOrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int IsPaid { get; set; }
        public int IsSended { get; set; }
        public double? DiscountPrice { get; set; }

        public ICollection<BOrderFoods> OrderFoods { get; set; }
        public ICollection<BUserOrders> UserOrders { get; set; }

        public food_order entityFoodOrder { get; set; }

        public BFoodOrder()
        {
            this.Reset();
        }

        public BFoodOrder(int objednavka, int jedlo, DateTimeOffset cas, risTabulky risContext)
        {
            entityFoodOrder = new food_order
            {
                food_order_id = objednavka,
                order_date = cas,
                discount_price = 0,
                is_paid = 0,
                is_sended = 0,
                total_price = 0
            };
            if (risContext != null)
            {
                risContext.food_order.Add(entityFoodOrder);
                risContext.SaveChanges();
            }
            FillBObject();
        }

        public BFoodOrder(food_order foodOrder)
        {
            FoodOrderId = foodOrder.food_order_id;
            OrderDate = foodOrder.order_date;
            TotalPrice = foodOrder.total_price;
            IsPaid = foodOrder.is_paid;
            IsSended = foodOrder.is_sended;
            if (foodOrder.discount_price != null) DiscountPrice = foodOrder.discount_price;

            OrderFoods = new List<BOrderFoods>();
            UserOrders = new List<BUserOrders>();

            foreach (var order_Foods1 in foodOrder.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_Foods1);
                OrderFoods.Add(pom);
            }

            foreach (var user_Orders1 in foodOrder.user_orders)
            {
                BUserOrders pom = new BUserOrders(user_Orders1);
                UserOrders.Add(pom);
            }

            entityFoodOrder = foodOrder;
        }

        private void Reset()
        {
            FoodOrderId = 0;
            OrderDate = DateTimeOffset.MinValue;
            TotalPrice = 0;
            IsPaid = 0;
            IsSended = 0;
            DiscountPrice = null;

            OrderFoods = new List<BOrderFoods>();
            UserOrders = new List<BUserOrders>();

            entityFoodOrder = new food_order();
        }

        private void FillBObject()
        {
            FoodOrderId = entityFoodOrder.food_order_id;
            OrderDate = entityFoodOrder.order_date;
            TotalPrice = entityFoodOrder.total_price;
            IsPaid = entityFoodOrder.is_paid;
            IsSended = entityFoodOrder.is_sended;
            if (entityFoodOrder.discount_price != null) DiscountPrice = entityFoodOrder.discount_price;

            OrderFoods = new List<BOrderFoods>();
            UserOrders = new List<BUserOrders>();

            foreach (var order_Foods1 in entityFoodOrder.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_Foods1);
                OrderFoods.Add(pom);
            }

            foreach (var user_Orders1 in entityFoodOrder.user_orders)
            {
                BUserOrders pom = new BUserOrders(user_Orders1);
                UserOrders.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityFoodOrder.food_order_id = FoodOrderId;
            entityFoodOrder.order_date = OrderDate;
            entityFoodOrder.total_price = TotalPrice;
            entityFoodOrder.is_paid = IsPaid;
            entityFoodOrder.is_sended = IsSended;
            entityFoodOrder.discount_price = DiscountPrice;

            foreach (var order_Foods1 in OrderFoods)
            {
                entityFoodOrder.order_foods.Add(order_Foods1.entityOrderFoods);
            }

            foreach (var user_Orders1 in UserOrders)
            {
                entityFoodOrder.user_orders.Add(user_Orders1.entityUserOrders);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodOrderId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food_order.Add(entityFoodOrder);
                    risContext.SaveChanges();
                    FoodOrderId = entityFoodOrder.food_order_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food_order where a.food_order_id == FoodOrderId select a;
                    entityFoodOrder = temp.Single();
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
                var temp = risContext.food_order.First(i => i.food_order_id == FoodOrderId);
                risContext.food_order.Remove(temp);
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
                var temp = from a in risContext.food_order where a.food_order_id == id select a;
                entityFoodOrder = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BFoodOrderCol : Dictionary<int, BFoodOrder>
        {
            private risTabulky risContext;

            public BFoodOrderCol(risTabulky r)
            {
                risContext = r;
            }

            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.food_order select a;
                    List<food_order> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_order_id, new BFoodOrder(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IList<TFoodOrder> ToTransferList()
            {
                IList<TFoodOrder> result = new List<TFoodOrder>();
                foreach (var objednavka in this)
                {
                    TFoodOrder objednavkaTemp = objednavka.Value.ToTransferObject();
                    result.Add(objednavkaTemp);
                }
                return result;
            }

            public TFoodOrder GetById(int id)
            {
                var temp = from a in risContext.food_order where a.food_order_id == id select a;
                var bObjednavka = new BFoodOrder(temp.ToList()[0]);
                return bObjednavka.ToTransferObject();
            }
        }

        public TFoodOrder ToTransferObject()
        {
            /* ercisk
            IList<TObjednavkaMenu> polozky = new List<TObjednavkaMenu>();
            foreach (var bObjednavkaMenu in objednavka_menu)
            {
                polozky.Add(bObjednavkaMenu.ToTransferObject());
            }
            TObjednavka tObjednavka = new TObjednavka(id_objednavky, id_stola, id_uctu, potvrdena, suma, polozky);
            if (datum_objednania != null)
            {
                tObjednavka.DatumObjednania = datum_objednania.Value.ToString();
            }
            if (datum_zaplatenia != null)
            {
                tObjednavka.DatumZaplatenia = datum_zaplatenia.Value.ToString();
            }
            */ // ercisk

            TFoodOrder tObjednavka = new TFoodOrder(FoodOrderId, OrderDate, TotalPrice, IsPaid, IsSended, DiscountPrice);
            return tObjednavka;
        }



    }
}
