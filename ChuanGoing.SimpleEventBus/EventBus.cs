using ChuanGoing.Base.Interface.Event;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChuanGoing.SimpleEventBus
{
    public class EventBus : IEventBus
    {
        private readonly EventQueue eventQueue = new EventQueue();
        private readonly IEnumerable<IEventHandler> eventHandlers;

        public EventBus(IEnumerable<IEventHandler> eventHandlers)
        {
            this.eventHandlers = eventHandlers;
        }

        /// <summary>
        /// 发布事件到队列时触发处理事件
        /// </summary>
        /// <param name="sendere"></param>
        /// <param name="e"></param>
        private void EventQueue_EventPushed(object sendere, EventProcessedEventArgs e)
        {
            (from eh in this.eventHandlers
             where
                eh.CanHandle(e.Event)
             select eh).ToList().ForEach(async eh => await eh.HandleAsync(e.Event));
        }

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken))
            where TEvent : IEvent
        => Task.Factory.StartNew(() => eventQueue.Push(@event));

        /// <summary>
        /// 事件订阅(订阅队列上的事件)
        /// </summary>
        public void Subscribe()
        {
            eventQueue.EventPushed += EventQueue_EventPushed;
        }
    }
}
