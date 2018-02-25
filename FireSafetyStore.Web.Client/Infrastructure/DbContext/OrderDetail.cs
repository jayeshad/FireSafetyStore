
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    public partial class OrderDetail
    {
        [Key]
        public Guid OrderDetailsId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public virtual OrderMaster OrderMaster { get; set; }

        public virtual Product Product { get; set; }
    }
}
