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
    }
}