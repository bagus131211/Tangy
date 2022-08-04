namespace TangyWeb.Client.Service
{
    using Tangy.Models;
    public interface IOrderService
    {
        Task<ICollection<OrderDTO>> GetAll(string? userId);
        Task<OrderDTO> GetById(int id);
        Task<OrderDTO> Create(StripePaymentDTO payment);
        Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeader);
    }
}
