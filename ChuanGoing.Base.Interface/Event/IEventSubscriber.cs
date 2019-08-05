namespace ChuanGoing.Base.Interface.Event
{
    /// <summary>
    /// 事件订阅者接口
    /// </summary>
    public interface IEventSubscriber
    {
        /// <summary>
        /// 事件订阅
        /// </summary>
        void Subscribe();
    }
}
