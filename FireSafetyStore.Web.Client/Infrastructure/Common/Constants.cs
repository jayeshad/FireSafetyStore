namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public class Constants
    {
        public const string FileStore = "~/FileStore/Images/";
        public const string CartSessionKey = "ShoppingCart";
        public const string PaymentSessionKey = "PaymentSessionKey";
        public enum AmountType
        {
            ProductCost = 0,
            ShippingCost = 1,
            TotalCost = 2
        }

        public class OrderStatuses
        {
            public const int NotYetDispatched = 0;
            public const int Dispatched = 1;
            public const int OutForDelivery = 2;
            public const int Delivered = 3;
        }

    }
}