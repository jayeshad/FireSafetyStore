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
            PaymentInformations = new HashSet<PaymentInformation>();
        }

        [Key]
        public Guid OrderId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Order Code")]
        public string OrderCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Customer Name")]
        public string CustomerFullName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "State")]
        public string CustomerState { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Country")]
        public string CustomerCountry { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Postal Code")]
        public string CustomerPostalCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string CustomerContactNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string ContactEmail { get; set; }
        [NotMapped]
        public string SelectedStatus { get; set; }
        [Display(Name = "Status of Order")]
        public int OrderStatus { get; set; }

        public bool? IsOrderCancelled { get; set; }

        [StringLength(500)]
        public string CancellationReason { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Courier Name")]
        public string DeliveryAgencyName { get; set; }

        [StringLength(50)]
        [Display(Name = "Courier Agent")]
        public string DeliveryAgentBoyName { get; set; }

        [StringLength(50)]
        [Display(Name = "Contact Number of Courier Agent")]
        public string DeliveryAgentContactNumber { get; set; }

        [StringLength(50)]
        public string NameInPaymentCard { get; set; }

        [StringLength(50)]
        public string PaymentCardNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal ShippingAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentInformation> PaymentInformations { get; set; }
    }
}
