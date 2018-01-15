using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using System.Collections.Generic;

namespace FireSafetyStore.Web.Client.Models
{
    public class CheckoutViewModel
    {
        public OrderMasterViewModel OrderMaster { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}