using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BTable
    {
        public string TableId { get; set; }

        public ICollection<BTableReservations> TableReservations { get; set; }

        public table entityTable { get; set; }

        public BTable()
        {
            this.Reset();
        }

        public BTable(table table)
        {
            TableId = table.table_id;

            TableReservations = new List<BTableReservations>();
            foreach (var table_Reservations1 in table.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }

            entityTable = table;
        }

        private void Reset()
        {
            TableId = "";
            TableReservations = new List<BTableReservations>();

            entityTable = new table();
        }

        private void FillBObject()
        {
            TableId = entityTable.table_id;

            TableReservations = new List<BTableReservations>();
            foreach (var table_Reservations1 in entityTable.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityTable.table_id = TableId;

            foreach (var table_Reservations1 in TableReservations)
            {
                entityTable.table_reservations.Add(table_Reservations1.entityTableReservations);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (TableId == "") // INSERT
                {
                    this.FillEntity();
                    risContext.table.Add(entityTable);
                    risContext.SaveChanges();
                    TableId = entityTable.table_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.table where a.table_id == TableId select a;
                    entityTable = temp.Single();
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
                var temp = risContext.table.First(i => i.table_id == TableId);
                risContext.table.Remove(temp);
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

        public bool Get(risTabulky risContext, string id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.table where a.table_id == id select a;
                entityTable = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTableCol : Dictionary<string, BTable>
        {

            public BTableCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.table select a;
                    List<table> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.table_id, new BTable(a));
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
