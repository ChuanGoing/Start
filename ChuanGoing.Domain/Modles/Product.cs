using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : DomainEntity<Guid>
    {
        /// <summary>
        /// 代号
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
    }
}
