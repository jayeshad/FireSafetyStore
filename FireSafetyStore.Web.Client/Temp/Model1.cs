namespace FireSafetyStore.Web.Client.Temp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<OrderData> OrderDatas { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
