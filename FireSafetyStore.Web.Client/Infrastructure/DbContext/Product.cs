namespace FireSafetyStore.Web.Client
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Product
    {
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

        [Display(Name = "Name of Brand")]    
        public Guid BrandId { get; set; }

        [Display(Name = "Product Category")]
        public Guid CategoryId { get; set; }

        [Display(Name = "Unit of Measure")]
        public Guid UnitId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal Rate { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "File Name")]
        public string OriginalFileName { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual UnitMaster UnitMaster { get; set; }
    }
}
