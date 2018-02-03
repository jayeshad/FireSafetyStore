namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaymentInformation")]
    public partial class PaymentInformation
    {
        [Key]
        public Guid PaymentId { get; set; }

        public Guid OrderId { get; set; }

        [Required]
        [StringLength(25)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(30)]
        public string CreditCardNo { get; set; }

        public int ExpiryDay { get; set; }

        public int ExpiryYear { get; set; }

        public int CvvCode { get; set; }

        public virtual OrderMaster OrderMaster { get; set; }
    }
}
