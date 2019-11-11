using ChuanGoing.Application.ViewModels;
using ChuanGoing.Base.Interface.Ioc;

namespace ChuanGoing.Application
{
    public interface IOrderService : IScopeInstance
    {
        void Add(OrderViewModel order);
        OrderViewResult Get(string sn);
    }
}
