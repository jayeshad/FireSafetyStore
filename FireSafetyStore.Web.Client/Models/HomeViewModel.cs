using System.Collections.Generic;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Models
{
    public class HomeViewModel
    {       
        public List<SelectListItem> BrandList { get; set; }
        public List<ItemViewModel> ProductList { get; set; }
    }
}