using System;
using System.Collections.Generic;

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
        public Guid UserId { get;private set; }
    }
}
