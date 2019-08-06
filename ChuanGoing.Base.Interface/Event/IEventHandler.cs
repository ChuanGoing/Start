using ChuanGoing.Base.Interface.Ioc;
using System.Threading;
using System.Threading.Tasks;

namespace ChuanGoing.Base.Interface.Event
{
    /// <summary>
    /// 事件处理器接口
    /// </summary>
    public interface IEventHandler : IDependencyInstance
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> HandleAsync(IEvent @event, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 可否处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        bool CanHandle(IEvent @event);
    }

    /// <summary>
    /// 泛型事件处理器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<T> : IEventHandler
        where T : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> HandleAsync(T @event, CancellationToken cancellationToken = default(CancellationToken));
    }
}
