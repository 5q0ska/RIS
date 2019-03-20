using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;
using TransferObjects;

namespace BiznisObjects
{

    public class BFood : TransferTemplate
    {
        public int FoodId { get; set; }
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public double PriceWithoutAdditions { get; set; }
        public int PreparationTime { get; set; }
        public double Weight { get; set; }
        public double? PriceWithAdditions { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public BFoodType FoodType { get; set; }
        public ICollection<BFoodAdditions> FoodAdditions { get; set; }
        public ICollection<BFoodAlergens> FoodAlergens { get; set; }
        public ICollection<BFoodRatings> FoodRatings { get; set; }
        public ICollection<BOrderFoods> OrderFoods { get; set; }

        public food entityFood { get; set; }

        public BFood()
        {
            this.Reset();
        }

        public BFood(food food)
        {
            FoodId = food.food_id;
            FoodTypeId = food.food_type_id;
            Name = food.name;
            PriceWithoutAdditions = food.price_without_additions;
            PreparationTime = food.preparation_time;
            Weight = food.weight;
            if (food.price_with_additions != null) PriceWithAdditions = food.price_with_additions;
            Description = food.description;
            Image = food.image;

            FoodType = new BFoodType(food.food_type);
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            foreach (var food_Additions1 in food.food_additions)
            {
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }

            foreach (var food_alergens1 in food.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_alergens1);
                FoodAlergens.Add(pom);
            }

            foreach (var food_ratings1 in food.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_ratings1);
                FoodRatings.Add(pom);
            }

            foreach (var order_foods1 in food.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_foods1);
                OrderFoods.Add(pom);
            }

            entityFood = food;
        }

        private void Reset()
        {
            FoodId = 0;
            FoodTypeId = 0;
            Name = "";
            PriceWithoutAdditions = 0;
            PreparationTime = 0;
            Weight = 0;
            PriceWithAdditions = null;
            Description = "";
            Image = new byte[65535];
            FoodType = new BFoodType();
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            entityFood = new food();
        }

        private void FillBObject()
        {
            FoodId = entityFood.food_id;
            FoodTypeId = entityFood.food_type_id;
            Name = entityFood.name;
            PriceWithoutAdditions = entityFood.price_without_additions;
            PreparationTime = entityFood.preparation_time;
            Weight = entityFood.weight;
            if (entityFood.price_with_additions != null) PriceWithAdditions = entityFood.price_with_additions;
            Description = entityFood.description;
            Image = entityFood.image;

            FoodType = new BFoodType(entityFood.food_type);
            FoodAdditions = new List<BFoodAdditions>();
            FoodAlergens = new List<BFoodAlergens>();
            FoodRatings = new List<BFoodRatings>();
            OrderFoods = new List<BOrderFoods>();

            foreach (var food_Additions1 in entityFood.food_additions)
            {
                BFoodAdditions pom = new BFoodAdditions(food_Additions1);
                FoodAdditions.Add(pom);
            }

            foreach (var food_alergens1 in entityFood.food_alergens)
            {
                BFoodAlergens pom = new BFoodAlergens(food_alergens1);
                FoodAlergens.Add(pom);
            }

            foreach (var food_ratings1 in entityFood.food_ratings)
            {
                BFoodRatings pom = new BFoodRatings(food_ratings1);
                FoodRatings.Add(pom);
            }

            foreach (var order_foods1 in entityFood.order_foods)
            {
                BOrderFoods pom = new BOrderFoods(order_foods1);
                OrderFoods.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityFood.food_id = FoodId;
            entityFood.food_type_id = FoodTypeId;
            entityFood.name = Name;
            entityFood.price_without_additions = PriceWithoutAdditions;
            entityFood.preparation_time = PreparationTime;
            entityFood.weight = Weight;
            entityFood.price_with_additions = PriceWithAdditions;
            entityFood.description = Description;
            entityFood.image = Image;

            entityFood.food_type = FoodType.entityFoodType;

            foreach (var food_Additions1 in FoodAdditions)
            {
                entityFood.food_additions.Add(food_Additions1.entityFoodAdditions);
            }

            foreach (var food_alergens1 in FoodAlergens)
            {
                entityFood.food_alergens.Add(food_alergens1.entityFoodAlergens);
            }

            foreach (var food_ratings1 in FoodRatings)
            {
                entityFood.food_ratings.Add(food_ratings1.entityFoodRatings);
            }

            foreach (var order_foods1 in OrderFoods)
            {
                entityFood.order_foods.Add(order_foods1.entityOrderFoods);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (FoodId == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.food.Add(entityFood);
                    risContext.SaveChanges();
                    FoodId = entityFood.food_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.food where a.food_id == FoodId select a;
                    entityFood = temp.Single();
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
                var temp = risContext.food.First(i => i.food_id == FoodId);
                risContext.food.Remove(temp);
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
                var temp = from a in risContext.food where a.food_id == id select a;
                entityFood = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        /// <summary>
        /// Zoznám jedal ako dictionary s id a jedlom
        /// </summary>
        public class BFoodCol : Dictionary<int, BFood>, TransferTemplateList
        {
            private risTabulky risContext;

            public BFoodCol(risTabulky risContext)
            {
                this.risContext = risContext;
            }

            /// <summary>
            /// Naplní zoznám jedal všetkými jedlami z databázy
            /// </summary>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.food select a;
                    List<food> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFood(a));
                    }

                    /*
                     * URCITE TREBA ODSTRANIT TIETO RIADKY - su pouzite len kvoli ziskaniu nejakej ceny
                     */
                     /* ercisk
                    var menuJedloQuery = from a in risContext.menu_jedlo select a;
                    List<menu_jedlo> menuJedloZoznam = menuJedloQuery.ToList();
                    foreach (var menuJedlo in menuJedloZoznam)
                    {
                        if (this.TryGetValue(menuJedlo.id_jedla, out BJedlo jedlo))
                        {
                            jedlo.Cena = menuJedlo.cena;
                        }
                    }
                    */ // ercisk

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// Naplní zoznam surovín všetkými jedlami v databáze ,ktorých názvy začínajú na text ,ktorý je parametrom
            /// </summary>
            /// <param name="startingString">text, na ktorý majú začínať nazvy , aspoň 3 písmena</param>
            /// <param name="risContext">kontext databázy</param>
            /// <returns>
            ///    <c>TRUE</c> , ak došlo k úspešnému načitaniu
            ///    <c>FALSE</c> , ak nedošlo k úspešenému načitaniu
            /// </returns>
            public bool GetNameStartingWith(String startingString)
            {
                try
                {
                    var jedla = from a in risContext.food where a.name.Contains(startingString) select a;


                    List<food> tempList = jedla.ToList();
                    this.Clear();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFood(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool GetFoodType(int foodType)
            {
                try
                {
                    var jedla = from a in risContext.food where a.food_type_id == foodType select a;


                    List<food> tempList = jedla.ToList();
                    this.Clear();
                    foreach (var a in tempList)
                    {
                        this.Add(a.food_id, new BFood(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IList<TransferEntity> toTransferList()
            {
                IList<TransferEntity> result = new List<TransferEntity>();
                foreach (var jedlo in this)
                {
                    var metadata = new byte[10];
                    if (jedlo.Value.Image != null)
                    {
                        metadata = jedlo.Value.Image;
                    }
                    TFood jedloTemp = new TFood(jedlo.Value.FoodId, jedlo.Value.FoodTypeId, jedlo.Value.Name, jedlo.Value.PriceWithoutAdditions, jedlo.Value.PreparationTime, jedlo.Value.Weight, jedlo.Value.PriceWithAdditions, jedlo.Value.Description, metadata);

                        jedloTemp.PriceWithoutAdditions = (double)jedlo.Value.PriceWithoutAdditions;

                    result.Add(jedloTemp);
                }
                return result;
            }
        }

        public TransferEntity toTransferObject()
        {
            TFood result = new TFood(FoodId, FoodTypeId, Name, PriceWithoutAdditions, PreparationTime, Weight, PriceWithAdditions, Description, Image);
            foreach (var surovina in FoodAdditions)
            {
                TAddition s = new TAddition(surovina.AdditionId, surovina.Addition.Name, surovina.Addition.Weight, surovina.Addition.Price, surovina.Addition.Description);
                result.PridajPrilohu(s);
            }
            return result;
        }

        public void updatefromTransferObject(TransferEntity transferEntity, risTabulky risContext)
        {
            if (transferEntity.GetType() == typeof(TFood))
            {
                TFood jedlo = (TFood)transferEntity;
                if (jedlo.FoodId != null)
                {
                    if (!jedlo.FoodTypeId.Equals(FoodTypeId))
                    {
                        entityFood.food_type_id = jedlo.FoodTypeId;
                    }

                    if (!jedlo.Name.Equals(Name))
                    {
                        entityFood.name = jedlo.Name;
                    }

                    if (!jedlo.PriceWithoutAdditions.Equals(PriceWithoutAdditions))
                    {
                        entityFood.price_without_additions = jedlo.PriceWithoutAdditions;
                    }

                    if (!jedlo.PreparationTime.Equals(PreparationTime))
                    {
                        entityFood.preparation_time = jedlo.PreparationTime;
                    }

                    if (!jedlo.Weight.Equals(Weight))
                    {
                        entityFood.weight = jedlo.Weight;
                    }

                    if (!jedlo.PriceWithAdditions.Equals(PriceWithAdditions))
                    {
                        entityFood.price_with_additions = jedlo.PriceWithAdditions;
                    }

                    if (!jedlo.Description.Equals(Description))
                    {
                        entityFood.description = jedlo.Description;
                    }

                    foreach (var surovina in FoodAdditions)
                    {
                        TAddition tempSurovina = jedlo.FoodAdditions.First(p => p.AdditionId == surovina.AdditionId);
                        if (tempSurovina != null)
                        {
                            if (!surovina.Addition.Price.Equals(tempSurovina.Price))
                            {
                                surovina.Addition.Price = tempSurovina.Price;
                            }
                        }
                        else
                        {
                            entityFood.food_additions.Remove(surovina.entityFoodAdditions);
                        }

                    }


                    foreach (var surovina in jedlo.FoodAdditions)
                    {
                        food_additions temp_bsurovina = entityFood.food_additions.First(p => p.addition_id == surovina.AdditionId);
                        if (temp_bsurovina == null)
                        {
                            entityFood.food_additions.Add(risContext.food_additions.First(p => p.addition_id == surovina.AdditionId));
                        }
                    }

                    /* ercisk
                    foreach (var preklad in jedlo)
                    {
                        preklad temp_preklad = entity.text.preklad.First(p => p.kod_jazyka.Equals(preklad.Key));
                        if (temp_preklad == null)
                        {
                            preklad prkl = new preklad();
                            prkl.kod_jazyka = preklad.Key;
                            prkl.preklad1 = preklad.Value;
                            prkl.text = nazov.entityText;

                            risContext.preklad.Add(prkl);
                            entity.text.preklad.Add(prkl);
                        }
                        else
                        {
                            temp_preklad.preklad1 = preklad.Value;
                        }
                    }

                    foreach (var preklad in nazov.preklad)
                    {
                        String temp_preklad = jedlo.Translations.Keys.First(p => p.Equals(preklad.kod_jazyka));
                        if (temp_preklad == null)
                        {
                            nazov.preklad.Remove(preklad);
                        }
                    }
                    */ //ercisk

                    risContext.SaveChanges();


                }
                else
                {
                    this.Reset();
                    entityFood = new food();

                    /* ercisk
                    entityFood. = jedlo.AmountOfCalories;
                    entityFood.dlzka_pripravy = jedlo.Length;

                    foreach (var suroviny in jedlo.RawMaterial)
                    {
                        surovina surovinaTemp = risContext.surovina.First(p => p.id_surovina == suroviny.Id);
                        if (surovinaTemp != null)
                        {
                            jedlo_surovina tempJedloSurovina = new jedlo_surovina();
                            tempJedloSurovina.jedlo = entity;
                            tempJedloSurovina.surovina = surovinaTemp;
                            tempJedloSurovina.mnozstvo = suroviny.Mnozstvo;
                            entityFood.jedlo_surovina.Add(tempJedloSurovina);
                        }
                    }

                    */ // ercisk

                }
            }
        }

    }
}
