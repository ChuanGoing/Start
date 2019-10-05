using System;
using System.Collections.Generic;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order : DomainEntity<Guid>
    {
        /// <summary>
        /// 订单流水号
        /// </summary>
        public string Sn { get; private set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; private set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; private set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public long PaymentTime { get; private set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public long ExpireTime { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; private set; }
        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItem> OrderItems { get; private set; }
    }
}
