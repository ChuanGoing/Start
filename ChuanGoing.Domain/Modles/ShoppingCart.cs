using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCart : DomainEntity<Guid>
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get;private set; }
    }
}
