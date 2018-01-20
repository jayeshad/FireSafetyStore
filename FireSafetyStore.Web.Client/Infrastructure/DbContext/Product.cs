namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public Guid ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public Guid SupplierId { get; set; }

        public Guid BrandId { get; set; }

        public Guid CategoryId { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? OfferExpiryDate { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        [StringLength(2500)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(1000)]
        public string OriginalFileName { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
