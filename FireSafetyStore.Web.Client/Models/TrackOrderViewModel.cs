using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireSafetyStore.Web.Client.Models
{
    public class TrackOrderViewModel
    {
        public Guid OrderId { get; set; }

        [Display(Name = "Order Code")]
        public string OrderCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Courier Agency")]
        public string DeliveryAgencyName { get; set; }
        
    }
}