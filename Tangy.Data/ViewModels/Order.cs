namespace Tangy.Data.ViewModels
{
    using Models;

    public class Order
    {
        public OrderHeader OrderHeader { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
