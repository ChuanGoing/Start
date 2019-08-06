using ChuanGoing.Base.Interface.Event;
using System;

namespace ChuanGoing.Web.API.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public CustomerCreatedEvent(string customerName)
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this.CustomerName = customerName;
        }

        public Guid Id { get; }

        public long Timestamp { get; }

        public string CustomerName { get; }
    }
}
