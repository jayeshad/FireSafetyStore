namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderMaster")]
    public partial class OrderMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderMaster()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public Guid OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime OrderDate { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerFullName { get; set; }

        [Required]
        [StringLength(500)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerState { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerCountry { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerPostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerContactNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactEmail { get; set; }

        public bool IsOrderConfirmed { get; set; }

        public bool? IsOrderCancelled { get; set; }

        [StringLength(500)]
        public string CancellationReason { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [StringLength(500)]
        public string DeliveryAgencyName { get; set; }

        [StringLength(50)]
        public string DeliveryAgentBoyName { get; set; }

        [StringLength(50)]
        public string DeliveryAgentContactNumber { get; set; }
        [StringLength(50)]
        public string NameInPaymentCard { get; set; }
        [StringLength(50)]
        public string PaymentCardNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal TotalAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
