using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace FireSafetyStore.Web.Client.Models
{
    public class CheckoutViewModel : IValidatableObject
    {
        public bool IsPaymentMode { get; set; }
        public OrderMasterViewModel OrderMaster { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(IsPaymentMode)
            {
                if(string.IsNullOrEmpty(OrderMaster.NameInPaymentCard))
                yield return new ValidationResult("ewrwer");

                if (string.IsNullOrEmpty(OrderMaster.PaymentCardNumber))
                    yield return new ValidationResult("werwer");

                if (string.IsNullOrEmpty(OrderMaster.CardTypes))
                    yield return new ValidationResult("ewrwer");

                if (OrderMaster.CvvCode > 0)
                    yield return new ValidationResult("werwerwer");
            }
        }
    }
}