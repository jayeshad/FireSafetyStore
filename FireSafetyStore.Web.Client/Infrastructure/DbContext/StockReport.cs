namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockReport")]
    public partial class StockReport
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Product { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Supplier { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Brand { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string StockStatus { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Stock { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal Rate { get; set; }
    }
}
