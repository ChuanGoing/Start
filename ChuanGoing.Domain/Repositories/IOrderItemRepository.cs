using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Ioc;
using ChuanGoing.Domain.Modles;
using System;

namespace ChuanGoing.Domain.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem, Guid>, IScopeInstance
    {
    }
}
