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

        public dining_table entityDiningTable { get; set; }

        public BTable()
        {
            this.Reset();
        }

        public BTable(dining_table dining_table)
        {
            TableId = dining_table.table_id;

            TableReservations = new List<BTableReservations>();
            foreach (var table_Reservations1 in dining_table.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }

            entityDiningTable = dining_table;
        }

        private void Reset()
        {
            TableId = "";
            TableReservations = new List<BTableReservations>();

            entityDiningTable = new dining_table();
        }

        private void FillBObject()
        {
            TableId = entityDiningTable.table_id;

            TableReservations = new List<BTableReservations>();
            foreach (var table_Reservations1 in entityDiningTable.table_reservations)
            {
                BTableReservations pom = new BTableReservations(table_Reservations1);
                TableReservations.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityDiningTable.table_id = TableId;

            foreach (var table_Reservations1 in TableReservations)
            {
                entityDiningTable.table_reservations.Add(table_Reservations1.entityTableReservations);
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
                    risContext.dining_table.Add(entityDiningTable);
                    risContext.SaveChanges();
                    TableId = entityDiningTable.table_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.dining_table where a.table_id == TableId select a;
                    entityDiningTable = temp.Single();
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
                var temp = risContext.dining_table.First(i => i.table_id == TableId);
                risContext.dining_table.Remove(temp);
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
                var temp = from a in risContext.dining_table where a.table_id == id select a;
                entityDiningTable = temp.Single();
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
                    var temp = from a in risContext.dining_table select a;
                    List<dining_table> tempList = temp.ToList();
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
