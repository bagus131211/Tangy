namespace Tangy.Models
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeader { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
