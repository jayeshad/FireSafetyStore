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
        public string ItemName { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public Guid BrandId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UnitId { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }                

        public int Stock { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual UnitMaster UnitMaster { get; set; }
    }
}
