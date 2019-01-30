using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BUserOrders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public BFoodOrder Order { get; set; }
        public BRisUser User { get; set; }

        public user_orders entityUserOrders { get; set; }

        public BUserOrders()
        {
            this.Reset();
        }

        public BUserOrders(user_orders tableReservations)
        {
            OrderId = tableReservations.order_id;
            UserId = tableReservations.user_id;

            Order = new BFoodOrder(tableReservations.order);
            User = new BRisUser(tableReservations.user);

            entityUserOrders = tableReservations;
        }

        private void Reset()
        {
            OrderId = 0;
            UserId = 0;

            Order = new BFoodOrder();
            User = new BRisUser();

            entityUserOrders = new user_orders();
        }

        private void FillBObject()
        {
            OrderId = entityUserOrders.order_id;
            UserId = entityUserOrders.user_id;

            Order = new BFoodOrder(entityUserOrders.order);
            User = new BRisUser(entityUserOrders.user);
        }

        private void FillEntity()
        {
            entityUserOrders.order_id = OrderId;
            entityUserOrders.user_id = UserId;
            entityUserOrders.order = Order.entityFoodOrder;
            entityUserOrders.user = User.entityRisUser;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (OrderId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.user_orders.Add(entityUserOrders);
                    risContext.SaveChanges();
                    OrderId = entityUserOrders.order_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.user_orders where a.order_id == OrderId && a.user_id == UserId select a;
                    entityUserOrders = temp.Single();
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
                var temp = risContext.user_orders.First(i => i.order_id == OrderId && i.user_id == UserId);
                risContext.user_orders.Remove(temp);
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
                var temp = from a in risContext.user_orders where a.order_id == id[0] && a.user_id == id[1] select a;
                entityUserOrders = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BUserOrdersCol : Dictionary<int, BUserOrders>
        {

            public BUserOrdersCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.user_orders select a;
                    List<user_orders> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.order_id, new BUserOrders(a));
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
