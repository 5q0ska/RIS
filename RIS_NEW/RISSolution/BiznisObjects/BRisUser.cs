using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BRisUser
    {
        public int RisUserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Image { get; set; }
        public double? DiscountPrice { get; set; }
        public int? ActualOrderId { get; set; }

        public ICollection<BFoodRating> FoodRating { get; set; }
        public ICollection<BTableReservations> TableReservations { get; set; }
        public ICollection<BUserOrders> UserOrders { get; set; }

        public ris_user entityRisUser { get; set; }

        public BRisUser()
        {
            this.Reset();
        }

        public BRisUser(ris_user risUser)
        {
            RisUserId = risUser.ris_user_id;
            Email = risUser.email;
            Password = risUser.password;
            Name = risUser.name;
            Surname = risUser.surname;
            Image = risUser.image;
            if (risUser.discount_price != null) DiscountPrice = risUser.discount_price;
            if (risUser.actual_order_id != null) ActualOrderId = risUser.actual_order_id;

            FoodRating = new List<BFoodRating>();
            TableReservations = new List<BTableReservations>();
            UserOrders = new List<BUserOrders>();

            foreach (var food_Rating1 in risUser.food_rating) { 
                BFoodRating pom = new BFoodRating(food_Rating1);
                FoodRating.Add(pom);
            }

            foreach (var table_Reservations1 in risUser.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }

            foreach (var user_Orders1 in risUser.user_orders)
            {
                BUserOrders pom = new BUserOrders(user_Orders1);
                UserOrders.Add(pom);
            }

            entityRisUser = risUser;
        }

        private void Reset()
        {
            RisUserId = 0;
            Email = "";
            Password = "";
            Name = "";
            Surname = "";
            Image = new byte[65535];
            DiscountPrice = null;
            ActualOrderId = null;

            FoodRating = new List<BFoodRating>();
            TableReservations = new List<BTableReservations>();
            UserOrders = new List<BUserOrders>();

            entityRisUser = new ris_user();
        }

        private void FillBObject()
        {
            RisUserId = entityRisUser.ris_user_id;
            Email = entityRisUser.email;
            Password = entityRisUser.password;
            Name = entityRisUser.name;
            Surname = entityRisUser.surname;
            Image = entityRisUser.image;
            if (entityRisUser.discount_price != null) DiscountPrice = entityRisUser.discount_price;
            if (entityRisUser.actual_order_id != null) ActualOrderId = entityRisUser.actual_order_id;

            FoodRating = new List<BFoodRating>();
            TableReservations = new List<BTableReservations>();
            UserOrders = new List<BUserOrders>();

            foreach (var food_Rating1 in entityRisUser.food_rating)
            {
                BFoodRating pom = new BFoodRating(food_Rating1);
                FoodRating.Add(pom);
            }

            foreach (var table_Reservations1 in entityRisUser.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }

            foreach (var user_Orders1 in entityRisUser.user_orders)
            {
                BUserOrders pom = new BUserOrders(user_Orders1);
                UserOrders.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityRisUser.ris_user_id = RisUserId;
            entityRisUser.email = Email;
            entityRisUser.password = Password;
            entityRisUser.name = Name;
            entityRisUser.surname = Surname;
            entityRisUser.image = Image;
            entityRisUser.discount_price = DiscountPrice;
            entityRisUser.actual_order_id = ActualOrderId;

            foreach (var food_Rating1 in FoodRating)
            {
                entityRisUser.food_rating.Add(food_Rating1.entityFoodRating);
            }

            foreach (var table_Reservations1 in TableReservations)
            {
                entityRisUser.table_reservations.Add(table_Reservations1.entityTableReservations);
            }

            foreach (var user_Orders1 in UserOrders)
            {
                entityRisUser.user_orders.Add(user_Orders1.entityUserOrders);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (RisUserId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.ris_user.Add(entityRisUser);
                    risContext.SaveChanges();
                    RisUserId = entityRisUser.ris_user_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.ris_user where a.ris_user_id == RisUserId select a;
                    entityRisUser = temp.Single();
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
                var temp = risContext.ris_user.First(i => i.ris_user_id == RisUserId);
                risContext.ris_user.Remove(temp);
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
                var temp = from a in risContext.ris_user where a.ris_user_id == id select a;
                entityRisUser = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BRisUserCol : Dictionary<int, BRisUser>
        {

            public BRisUserCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.ris_user select a;
                    List<ris_user> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.ris_user_id, new BRisUser(a));
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
