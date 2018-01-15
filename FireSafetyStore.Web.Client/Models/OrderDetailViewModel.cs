using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireSafetyStore.Web.Client.Models
{
    public class OrderDetailViewModel
    {
        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Total { get; set; }
    }
}