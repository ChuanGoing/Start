using ChuanGoing.Base.Interface.Domain;
using System;

namespace ChuanGoing.Domain
{
    public class DomainEvent<TPrimaryKey> : IDomainEvent<TPrimaryKey>
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public Guid Id { get; }
        public long Timestamp { get; }
        public TPrimaryKey DomainEntityId { get; set; }
        public string DomainEntityType { get; set; }
        public long Sequence { get; set; }

    }
}
