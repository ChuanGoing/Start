using ChuanGoing.Application.ViewModels;
using ChuanGoing.Base.Features;
using ChuanGoing.Base.Interface.Ioc;

namespace ChuanGoing.Application
{
    public interface IOrderService : IScopeInstance
    {
        [UnitOfWork]
        void Add(OrderViewModel order);
        OrderViewResult Get(string sn);
    }
}
