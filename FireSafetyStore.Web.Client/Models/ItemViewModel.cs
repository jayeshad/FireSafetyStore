using System;

namespace FireSafetyStore.Web.Client.Models
{
    public class ItemViewModel
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}