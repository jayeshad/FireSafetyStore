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
        [Display(Name = "Product Name")]
        public string ItemName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public Guid SupplierId { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public Guid BrandId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal DealerPrice { get; set; }

        [Required]
        public decimal Rate { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? OfferExpiryDate { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public string OriginalFileName { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
