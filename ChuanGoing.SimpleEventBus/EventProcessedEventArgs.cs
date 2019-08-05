using ChuanGoing.Base.Interface.Event;
using System;

namespace ChuanGoing.SimpleEventBus
{
    /// <summary>
    /// 消息事件参数
    /// </summary>
    public class EventProcessedEventArgs : EventArgs
    {
        public IEvent Event { get; }

        public EventProcessedEventArgs(IEvent @event)
        {
            Event = @event;
        }
    }
}
