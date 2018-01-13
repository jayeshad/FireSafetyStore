using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireSafetyStore.Web.Client.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            ProductList = new List<ItemViewModel>();
        }
        public List<ItemViewModel> ProductList { get; set; }
    }

    public class ItemViewModel
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}