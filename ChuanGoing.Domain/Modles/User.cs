using System;

namespace ChuanGoing.Domain.Modles
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : DomainEntity<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; private set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public int BirthDate { get; private set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; private set; }
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
