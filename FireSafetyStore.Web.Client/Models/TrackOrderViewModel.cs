using System;

namespace FireSafetyStore.Web.Client.Models
{
    public class TrackOrderViewModel
    {
        public Guid OrderId { get; internal set; }
        public string OrderCode { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public string OrderStatus { get; internal set; }
        public DateTime? DeliveryDate { get; internal set; }
        public string DeliveryAgencyName { get; internal set; }
    }
}