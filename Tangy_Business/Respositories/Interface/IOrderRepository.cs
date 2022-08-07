namespace Tangy.Business.Respositories.Interface
{
    using Models;
    public interface IOrderRepository
    {
        Task<OrderDTO> GetById(int id);
        Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null);
        Task<OrderDTO> Create(OrderDTO order);
        Task<int> Delete(int id);
        Task<OrderHeaderDTO> UpdateOrderHeader(OrderHeaderDTO orderHeader);
        Task<OrderHeaderDTO> MarkPaymentAsSuccessful(int id);
        Task<bool> UpdateOrderStatus(int id, string status);
        Task<OrderHeaderDTO> CancelOrder(int id);
    }
}
