using ChuanGoing.Base.Interface.Event;
using System;

namespace ChuanGoing.SimpleEventBus
{
    internal sealed class EventQueue
    {
        public event EventHandler<EventProcessedEventArgs> EventPushed;

        public EventQueue()
        {

        }

        public void Push(IEvent @event)
        {
            OnMessagePushed(new EventProcessedEventArgs(@event));
        }

        private void OnMessagePushed(EventProcessedEventArgs e)
        {
            this.EventPushed?.Invoke(this, e);
        }
    }
}
