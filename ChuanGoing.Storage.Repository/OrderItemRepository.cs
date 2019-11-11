using Autofac;
using ChuanGoing.Domain.Modles;
using ChuanGoing.Domain.Repositories;
using ChuanGoing.Storage.Dapper;
using System;

namespace ChuanGoing.Storage.Repository
{
    public class OrderItemRepository : DapperRepository<OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(IComponentContext container, IDapperDbContext dbContext)
            : base(container, dbContext)
        {

        }
    }
}
