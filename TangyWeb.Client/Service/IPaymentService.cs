using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public interface IPaymentService
    {
        Task<SuccessDTO> Checkout(StripePaymentDTO payment);
    }
}
