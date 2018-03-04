namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System.Data.Entity;

    public partial class FiresafeDbContext : DbContext
    {
        public FiresafeDbContext() : base("name=FiresafeDbContext")
        {
        }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderMaster> OrderMasters { get; set; }
        public virtual DbSet<PaymentInformation> PaymentInformations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<SalesReport> SalesReports { get; set; }
        public virtual DbSet<StockReport> StockReports { get; set; }
        public virtual DbSet<OrderData> OrderDatas { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CardType>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.CardTypes)
                .HasForeignKey(e => e.CardType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderMaster>()
                .Property(e => e.OrderAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderMaster>()
                .Property(e => e.ShippingAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderMaster>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderMaster>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.OrderMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderMaster>()
                .HasMany(e => e.PaymentInformations)
                .WithRequired(e => e.OrderMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<SalesReport>()
                .Property(e => e.OrderDate)
                .IsUnicode(false);

            modelBuilder.Entity<SalesReport>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SalesReport>()
                .Property(e => e.ProfitEarned)
                .HasPrecision(19, 2);

            modelBuilder.Entity<StockReport>()
                .Property(e => e.StockStatus)
                .IsUnicode(false);

            modelBuilder.Entity<OrderData>()
                .Property(e => e.Rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderData>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .Property(e => e.OrderAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .Property(e => e.ShippingAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);
        }
    }
}
