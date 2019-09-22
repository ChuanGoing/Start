using ChuanGoing.Base.Interface.Db;

namespace ChuanGoing.Base.Interface.Domain
{
    /// <summary>
    /// 聚合根接口定义
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {
    }
}
