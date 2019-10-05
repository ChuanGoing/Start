using ChuanGoing.Base.Domain;
using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 购物车明细
    /// </summary>
    public class ShoppingCartItem : Entity<Guid>
    {
        /// <summary>
        /// 购物车
        /// </summary>
        public ShoppingCart ShoppingCart { get; private set; }
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
