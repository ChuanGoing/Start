using System;

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
        /// 用户id
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// 更新者id
        /// </summary>
        public Guid UpdaterId { get; private set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; private set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        public Guid CreatorId { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; private set; }
    }
}
