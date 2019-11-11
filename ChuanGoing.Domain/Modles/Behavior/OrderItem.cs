namespace ChuanGoing.Domain.Modles
{
    public partial class OrderItem
    {
        public void SetOrder(Order order)
        {
            OrderId = order.Id;
        }
    }
}
