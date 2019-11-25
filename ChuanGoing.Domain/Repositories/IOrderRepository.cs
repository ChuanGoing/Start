using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Ioc;
using ChuanGoing.Domain.Modles;
using System;

namespace ChuanGoing.Domain.Repositories
{
    public interface IOrderRepository : ICommandRepository<Order, Guid>, IScopeInstance
    {
        Order GetBySn(string sn);
    }
}
