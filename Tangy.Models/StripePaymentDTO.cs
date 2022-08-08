//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Tangy.Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            SuccessUrl = "OrderConfirmation";
            CancelUrl = "Summary";
        }
        //not all mvc packages works with blazor as it will return some error if against blazor webassembly
        //[ValidateNever]
        public OrderDTO Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
