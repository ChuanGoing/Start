using ChuanGoing.Base.Domain;
using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class OrderItem : Entity<Guid>
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Order Order { get; private set; }
        /// <summary>
        /// 商品
        /// </summary>
        public Product Product { get; private set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public long JoinTime { get; private set; }
    }
}
