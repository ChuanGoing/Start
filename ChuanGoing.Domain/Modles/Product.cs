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
