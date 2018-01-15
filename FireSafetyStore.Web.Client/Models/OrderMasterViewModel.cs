using System;
using System.ComponentModel.DataAnnotations;

namespace FireSafetyStore.Web.Client.Models
{
    public class OrderMasterViewModel
    {

        public Guid OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }

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

        public DateTime? DeliveryDate { get; set; }

        [StringLength(500)]
        public string DeliveryAgencyName { get; set; }

        [StringLength(50)]
        public string DeliveryAgentBoyName { get; set; }

        [StringLength(50)]
        public string DeliveryAgentContactNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NameInPaymentCard { get; set; }
        
        [Required]
        [StringLength(50)]
        public string PaymentCardNumber { get; set; }

        public string CardType { get; set; }
        [Required]
        public int CvvCode { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal TotalAmount { get; set; }

    }
}