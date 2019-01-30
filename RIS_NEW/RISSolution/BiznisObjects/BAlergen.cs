using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BAlergen
    {
        public int AlergenId { get; set; }
        public string Description { get; set; }

        public ICollection<BFoodAlergens> FoodAlergens { get; set; }

        public alergen entityAlergen { get; set; }

        public BAlergen()
        {
            this.Reset();
        }

        public BAlergen(alergen alergen)
        {
            AlergenId = alergen.alergen_id;
            Description = alergen.description;

            FoodAlergens = new List<BFoodAlergens>();
            foreach (var food_Alergen1 in alergen.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_Alergen1);
                FoodAlergens.Add(pom);
            }

            entityAlergen = alergen;
        }

        private void Reset()
        {
            AlergenId = 0;
            Description = "";
            FoodAlergens = new List<BFoodAlergens>();

            entityAlergen = new alergen();
        }

        private void FillBObject()
        {
            AlergenId = entityAlergen.alergen_id;
            Description = entityAlergen.description;

            FoodAlergens = new List<BFoodAlergens>();
            foreach (var food_Alergens1 in entityAlergen.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_Alergens1);
                FoodAlergens.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityAlergen.alergen_id = AlergenId;
            entityAlergen.description = Description;

            foreach (var food_Alergens1 in FoodAlergens)
            {
                entityAlergen.food_alergens.Add(food_Alergens1.entityFoodAlergens);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (AlergenId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.alergen.Add(entityAlergen);
                    risContext.SaveChanges();
                    AlergenId = entityAlergen.alergen_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.alergen where a.alergen_id == AlergenId select a;
                    entityAlergen = temp.Single();
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
                var temp = risContext.alergen.First(i => i.alergen_id == AlergenId);
                risContext.alergen.Remove(temp);
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
                var temp = from a in risContext.alergen where a.alergen_id == id select a;
                entityAlergen = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BAlergenCol : Dictionary<int, BAlergen>
        {

            public BAlergenCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.alergen select a;
                    List<alergen> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.alergen_id, new BAlergen(a));
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
