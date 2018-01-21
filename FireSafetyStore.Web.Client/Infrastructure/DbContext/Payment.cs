namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Payment
    {
        public Guid PaymentId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CardType { get; set; }

        public int CvvCode { get; set; }
    }
}
