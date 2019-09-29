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
        /// 购物车id
        /// </summary>
        public Guid ShoppingCartId { get; private set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public Guid ProductId { get; private set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public long CreateTime { get; private set; }
    }
}
