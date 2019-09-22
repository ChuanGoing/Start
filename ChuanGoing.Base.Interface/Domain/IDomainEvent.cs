using ChuanGoing.Base.Interface.Event;

namespace ChuanGoing.Base.Interface.Domain
{
    /// <summary>
    /// 领域事件接口定义
    /// </summary>
    public interface IDomainEvent<TPrimaryKey> : IEvent
    {
        TPrimaryKey DomainEntityId { get; set; }
        string DomainEntityType { get; set; }
        long Sequence { get; set; }
    }
}
