
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    [Table("OrderData")]
    public partial class OrderData
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "money")]
        public decimal Rate { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "money")]
        public decimal Total { get; set; }
    }
}
