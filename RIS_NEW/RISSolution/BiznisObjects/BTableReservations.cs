using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BTableReservations
    {
        public string TableId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public BTable Table { get; set; }
        public BRisUser User { get; set; }

        public table_reservations entityTableReservations { get; set; }

        public BTableReservations()
        {
            this.Reset();
        }

        public BTableReservations(table_reservations tableReservations)
        {
            TableId = tableReservations.table_id;
            UserId = tableReservations.user_id;
            DateTime = tableReservations.date_time;

            Table = new BTable(tableReservations.table);
            User = new BRisUser(tableReservations.user);

            entityTableReservations = tableReservations;
        }

        private void Reset()
        {
            TableId = "";
            UserId = 0;
            DateTime = DateTimeOffset.MinValue;

            Table = new BTable();
            User = new BRisUser();

            entityTableReservations = new table_reservations();
        }

        private void FillBObject()
        {
            TableId = entityTableReservations.table_id;
            UserId = entityTableReservations.user_id;
            DateTime = entityTableReservations.date_time;

            Table = new BTable(entityTableReservations.table);
            User = new BRisUser(entityTableReservations.user);
        }

        private void FillEntity()
        {
            entityTableReservations.table_id = TableId;
            entityTableReservations.user_id = UserId;
            entityTableReservations.date_time = DateTime;
            entityTableReservations.table = Table.entityTable;
            entityTableReservations.user = User.entityRisUser;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (TableId == "") // INSERT
                {
                    this.FillEntity();
                    risContext.table_reservations.Add(entityTableReservations);
                    risContext.SaveChanges();
                    TableId = entityTableReservations.table_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.table_reservations where a.table_id == TableId && a.user_id == UserId select a;
                    entityTableReservations = temp.Single();
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
                var temp = risContext.table_reservations.First(i => i.table_id == TableId && i.user_id == UserId);
                risContext.table_reservations.Remove(temp);
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

        public bool Get(risTabulky risContext, string[] id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.table_reservations where a.table_id == id[0] && a.user_id == int.Parse(id[1]) select a;
                entityTableReservations = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTableReservationsCol : Dictionary<string, BTableReservations>
        {

            public BTableReservationsCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.table_reservations select a;
                    List<table_reservations> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.table_id, new BTableReservations(a));
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
