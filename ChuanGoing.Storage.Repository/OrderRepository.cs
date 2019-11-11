using Autofac;
using ChuanGoing.Domain.Modles;
using ChuanGoing.Domain.Repositories;
using ChuanGoing.Storage.Dapper;
using System;
using System.Linq;

namespace ChuanGoing.Storage.Repository
{
    public class OrderRepository : DapperRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(IComponentContext container, IDapperDbContext dbContext)
            : base(container, dbContext)
        {

        }

        public Order GetBySn(string sn)
        {
            var order = QuerySingleOrDefault<Order>("SELECT * FROM `Order` WHERE `Sn`=@Sn;", new
            {
                Sn = sn
            });
            if (order != null)
            {
                order.SetItems(Query<OrderItem>("SELECT * FROM `OrderItem` WHERE `OrderId`=@OrderId;", new
                {
                    OrderId = order.Id
                }).ToList());
            }
            return order;
        }
    }
}
