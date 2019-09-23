using ChuanGoing.Base.Features;
using ChuanGoing.Base.Interface.Domain;
using ChuanGoing.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ChuanGoing.Domain
{
    public class DomainEntity<TPrimaryKey> : IDomainEntity<TPrimaryKey>
    {
        private readonly Lazy<Dictionary<string, MethodInfo>> _registerHandlers;
        private readonly Queue<IDomainEvent<TPrimaryKey>> _unCommitEvents = new Queue<IDomainEvent<TPrimaryKey>>();
        private readonly object sync = new object();

        private TPrimaryKey _id;
        private long _persistedVersion = 0;


        protected DomainEntity()
            : this(GenerateIdHelper.NewId<TPrimaryKey>())
        {

        }

        protected DomainEntity(TPrimaryKey id)
        {
            _registerHandlers = new Lazy<Dictionary<string, MethodInfo>>(() =>
            {
                //注册内联事件处理(当前领域实体任何内联事件产生时自动捕获事件并处理)
                var registry = new Dictionary<string, MethodInfo>();
                var methodInfoList = from mi in this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     let returnType = mi.ReturnType
                                     let parameters = mi.GetParameters()
                                     where mi.IsDefined(typeof(HandlesInlineAttribute), false) &&
                                     returnType == typeof(void) &&
                                     parameters.Length == 1 &&
                                     typeof(IDomainEvent<TPrimaryKey>).IsAssignableFrom(parameters[0].ParameterType)
                                     select new { EventName = parameters[0].ParameterType.FullName, MethodInfo = mi };

                foreach (var methodInfo in methodInfoList)
                {
                    registry.Add(methodInfo.EventName, methodInfo.MethodInfo);
                }

                return registry;
            });

            Raise(new DomainEntityCreatedEvent<TPrimaryKey>(id));
        }


        public IEnumerable<IDomainEvent<TPrimaryKey>> UnCommitEvents => _unCommitEvents;

        public long Version => _unCommitEvents.Count + _persistedVersion;

        public TPrimaryKey Id => _id;

        public long PersistVersion { set => Interlocked.Exchange(ref _persistedVersion, value); }

        private void HandleEvent<TDomainEvent>(TDomainEvent domainEvent)
           where TDomainEvent : IDomainEvent<TPrimaryKey>
        {
            var key = domainEvent.GetType().FullName;
            if (_registerHandlers.Value.ContainsKey(key))
            {
                _registerHandlers.Value[key].Invoke(this, new object[] { domainEvent });
            }
        }

        [HandlesInline]
        protected void OnAggregateCreated(DomainEntityCreatedEvent<TPrimaryKey> @event)
        {
            _id = @event.NewId;
        }

        protected void Raise<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent<TPrimaryKey>
        {
            lock (sync)
            {
                // 首先处理事件数据。
                this.HandleEvent(domainEvent);

                // 然后设置事件的元数据，包括当前事件所对应的聚合根类型以及
                // 聚合的ID值。
                domainEvent.DomainEntityId = _id;
                domainEvent.DomainEntityType = this.GetType().AssemblyQualifiedName;

                domainEvent.Sequence = this.Version + 1;

                // 最后将事件缓存在“未提交事件”列表中。
                this._unCommitEvents.Enqueue(domainEvent);
            }
        }

        void IPurgable.Purge()
        {
            lock (sync)
            {
                _unCommitEvents.Clear();
            }
        }

        public void Replay(IEnumerable<IDomainEvent<TPrimaryKey>> events)
        {
            ((IPurgable)this).Purge();
            events.OrderBy(e => e.Timestamp)
                .ToList()
                .ForEach(e =>
                {
                    HandleEvent(e);
                    Interlocked.Increment(ref _persistedVersion);
                });
        }

        #region MyRegion

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainEntity<TPrimaryKey>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (DomainEntity<TPrimaryKey>)obj;

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }

        public static bool operator ==(DomainEntity<TPrimaryKey> left, DomainEntity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }
        public static bool operator !=(DomainEntity<TPrimaryKey> left, DomainEntity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
