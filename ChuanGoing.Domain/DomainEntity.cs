using ChuanGoing.Base.Interface.Domain;
using ChuanGoing.Base.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace ChuanGoing.Domain
{
    public class DomainEntity<TPrimaryKey> : IDomainEntity<TPrimaryKey>
    {
        private readonly LazyConcurrentDictionary<string, MethodInfo> _registerHandlers;
        private readonly Queue<IDomainEvent<TPrimaryKey>> _unCommitEvents = new Queue<IDomainEvent<TPrimaryKey>>();

        private TPrimaryKey _id;
        private long _persistedVersion = 0;

        protected DomainEntity()
            : this(GenerateIdHelper.NewId<TPrimaryKey>())
        {

        }

        protected DomainEntity(TPrimaryKey id)
        {

        }


        public IEnumerable<IDomainEvent<TPrimaryKey>> UnCommitEvents => _unCommitEvents;

        public long Version => _unCommitEvents.Count + _persistedVersion;

        public TPrimaryKey Id => _id;

        public long PersistVersion { set => Interlocked.Exchange(ref _persistedVersion, value); }

        public void Purge()
        {
            throw new System.NotImplementedException();
        }

        public void Replay(IEnumerable<IDomainEvent<TPrimaryKey>> events)
        {
            throw new System.NotImplementedException();
        }
    }
}
