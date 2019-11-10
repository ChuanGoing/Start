namespace ChuanGoing.Domain.Modles
{
    public partial class OrderItem
    {
        public void SetOrder(Order order)
        {
            OrderId = order.Id;
        }

        ///// <summary>
        ///// 订单编号
        ///// </summary>
        //public Guid OrderId { get; private set; }
        ///// <summary>
        ///// 商品编号
        ///// </summary>
        //public Guid ProductId { get; private set; }
        ///// <summary>
        ///// 商品单价
        ///// </summary>
        //public decimal Price { get; private set; }
        ///// <summary>
        ///// 数量
        ///// </summary>
        //public int Count { get; private set; }
        ///// <summary>
        ///// 加入时间
        ///// </summary>
        //public long JoinTime { get; private set; }
        public void Add()
        {

        }
    }
}
