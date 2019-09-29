using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCart : DomainEntity<Guid>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }
    }
}
