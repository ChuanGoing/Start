using System;

namespace ChuanGoing.Base.Interface.Event
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IEvent
    {
        Guid Id { get; }
        /// <summary>
        /// 时间戳
        /// </summary>
        long Timestamp { get; }
    }
}
