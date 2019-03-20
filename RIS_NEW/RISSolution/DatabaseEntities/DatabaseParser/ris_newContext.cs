using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RISServer.Model
{
    public partial class ris_newContext : DbContext
    {
        public ris_newContext()
        {
        }

        public ris_newContext(DbContextOptions<ris_newContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addition> Addition { get; set; }
        public virtual DbSet<Alergen> Alergen { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodAdditions> FoodAdditions { get; set; }
        public virtual DbSet<FoodAlergens> FoodAlergens { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<FoodRating> FoodRating { get; set; }
        public virtual DbSet<FoodRatings> FoodRatings { get; set; }
        public virtual DbSet<FoodType> FoodType { get; set; }
        public virtual DbSet<OrderFoods> OrderFoods { get; set; }
        public virtual DbSet<RisUser> RisUser { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<TableReservations> TableReservations { get; set; }
        public virtual DbSet<UserOrders> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=158.193.144.172;user id=developer1;Database=ris_new;password=risproject;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Addition>(entity =>
            {
                entity.ToTable("addition", "ris_new");

                entity.Property(e => e.AdditionId)
                    .HasColumnName("Addition_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Alergen>(entity =>
            {
                entity.ToTable("alergen", "ris_new");

                entity.Property(e => e.AlergenId)
                    .HasColumnName("Alergen_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food", "ris_new");

                entity.HasIndex(e => e.FoodTypeId)
                    .HasName("Food_Type_Id");

                entity.Property(e => e.FoodId)
                    .HasColumnName("Food_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FoodTypeId)
                    .HasColumnName("Food_Type_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Image).HasColumnType("blob");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PreparationTime)
                    .HasColumnName("Preparation_Time")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PriceWithAdditions).HasColumnName("Price_With_Additions");

                entity.Property(e => e.PriceWithoutAdditions).HasColumnName("Price_Without_Additions");

                entity.HasOne(d => d.FoodType)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.FoodTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_ibfk_1");
            });

            modelBuilder.Entity<FoodAdditions>(entity =>
            {
                entity.HasKey(e => new { e.AdditionId, e.FoodId });

                entity.ToTable("food_additions", "ris_new");

                entity.HasIndex(e => e.FoodId)
                    .HasName("Food_Id");

                entity.Property(e => e.AdditionId)
                    .HasColumnName("Addition_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FoodId)
                    .HasColumnName("Food_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Addition)
                    .WithMany(p => p.FoodAdditions)
                    .HasForeignKey(d => d.AdditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_additions_ibfk_1");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodAdditions)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_additions_ibfk_2");
            });

            modelBuilder.Entity<FoodAlergens>(entity =>
            {
                entity.HasKey(e => new { e.AlergenId, e.FoodId });

                entity.ToTable("food_alergens", "ris_new");

                entity.HasIndex(e => e.FoodId)
                    .HasName("Food_Id");

                entity.Property(e => e.AlergenId)
                    .HasColumnName("Alergen_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FoodId)
                    .HasColumnName("Food_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Alergen)
                    .WithMany(p => p.FoodAlergens)
                    .HasForeignKey(d => d.AlergenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_alergens_ibfk_1");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodAlergens)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_alergens_ibfk_2");
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.ToTable("food_order", "ris_new");

                entity.Property(e => e.FoodOrderId)
                    .HasColumnName("Food_Order_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.DiscountPrice).HasColumnName("Discount_Price");

                entity.Property(e => e.IsPaid)
                    .HasColumnName("Is_Paid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsSended)
                    .HasColumnName("Is_Sended")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("Order_Date")
                    .HasColumnType("timestamp(6)");

                entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");
            });

            modelBuilder.Entity<FoodRating>(entity =>
            {
                entity.ToTable("food_rating", "ris_new");

                entity.HasIndex(e => e.UserId)
                    .HasName("User_Id");

                entity.Property(e => e.FoodRatingId)
                    .HasColumnName("Food_Rating_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.RatingComment)
                    .HasColumnName("Rating_Comment")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StarsCount)
                    .HasColumnName("Stars_Count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FoodRating)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("food_rating_ibfk_1");
            });

            modelBuilder.Entity<FoodRatings>(entity =>
            {
                entity.HasKey(e => new { e.FoodRatingId, e.FoodId });

                entity.ToTable("food_ratings", "ris_new");

                entity.HasIndex(e => e.FoodId)
                    .HasName("Food_Id");

                entity.Property(e => e.FoodRatingId)
                    .HasColumnName("Food_Rating_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FoodId)
                    .HasColumnName("Food_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodRatings)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_ratings_ibfk_2");

                entity.HasOne(d => d.FoodRating)
                    .WithMany(p => p.FoodRatings)
                    .HasForeignKey(d => d.FoodRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("food_ratings_ibfk_1");
            });

            modelBuilder.Entity<FoodType>(entity =>
            {
                entity.ToTable("food_type", "ris_new");

                entity.Property(e => e.FoodTypeId)
                    .HasColumnName("Food_Type_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderFoods>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.OrderId });

                entity.ToTable("order_foods", "ris_new");

                entity.HasIndex(e => e.OrderId)
                    .HasName("Order_Id");

                entity.Property(e => e.FoodId)
                    .HasColumnName("Food_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderFoods)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_foods_ibfk_1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderFoods)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_foods_ibfk_2");
            });

            modelBuilder.Entity<RisUser>(entity =>
            {
                entity.ToTable("ris_user", "ris_new");

                entity.Property(e => e.RisUserId)
                    .HasColumnName("RIS_User_Id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActualOrderId)
                    .HasColumnName("Actual_Order_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountPrice).HasColumnName("Discount_Price");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasColumnType("blob");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("table", "ris_new");

                entity.Property(e => e.TableId)
                    .HasColumnName("Table_Id")
                    .HasColumnType("char(20)")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<TableReservations>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.UserId });

                entity.ToTable("table_reservations", "ris_new");

                entity.HasIndex(e => e.UserId)
                    .HasName("User_Id");

                entity.Property(e => e.TableId)
                    .HasColumnName("Table_Id")
                    .HasColumnType("char(20)");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateTime)
                    .HasColumnName("Date_Time")
                    .HasColumnType("timestamp(6)");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableReservations)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_reservations_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TableReservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("table_reservations_ibfk_2");
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.UserId });

                entity.ToTable("user_orders", "ris_new");

                entity.HasIndex(e => e.UserId)
                    .HasName("User_Id");

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_orders_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_orders_ibfk_2");
            });
        }
    }
}
