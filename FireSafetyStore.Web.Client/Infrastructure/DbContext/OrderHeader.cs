namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderHeader")]
    public partial class OrderHeader
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string OrderCode { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OrderDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string CustomerFullName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string CustomerAddress { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string CustomerState { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string CustomerCountry { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string CustomerPostalCode { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string CustomerContactNumber { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string ContactEmail { get; set; }

        [Key]
        [Column(Order = 10)]
        public DateTime DeliveryDate { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(500)]
        public string DeliveryAgencyName { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(50)]
        public string DeliveryAgentBoyName { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string DeliveryAgentContactNumber { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(50)]
        public string NameInPaymentCard { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(50)]
        public string PaymentCardNumber { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "money")]
        public decimal OrderAmount { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "money")]
        public decimal ShippingAmount { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "money")]
        public decimal TotalAmount { get; set; }
    }
}
