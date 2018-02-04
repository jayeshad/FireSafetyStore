namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesReport")]
    public partial class SalesReport
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string OrderCode { get; set; }

        [StringLength(10)]
        public string OrderDate { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Price { get; set; }

        public decimal? ProfitEarned { get; set; }
    }
}
