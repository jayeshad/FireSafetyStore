using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Models
{
    public class ShoppingCartViewModel
    {
        public List<ItemViewModel> ShoppingCartItems { get; set; }
        public List<SelectListItem> CardType { get; set; }

    }
}