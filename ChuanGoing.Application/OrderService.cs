using Autofac;
using ChuanGoing.Application.ViewModels;
using ChuanGoing.Base.Exceptions;
using ChuanGoing.Domain.Modles;
using ChuanGoing.Domain.Repositories;
using System;

namespace ChuanGoing.Application
{
    public class OrderService : ServiceBase<Order, Guid>, IOrderService
    {
        // 注入仓储接口
        private readonly IOrderRepository _repo;
        private readonly IOrderItemRepository _itemRepo;

        public OrderService(IComponentContext container,
            IOrderRepository repo, IOrderItemRepository itemRepo)
            : base(container, repo)
        {
            _repo = repo;
            _itemRepo = itemRepo;
        }

        public void Add(OrderViewModel view)
        {
            if (view == null)
            {
                throw new InnerException(MessageCodes.InvalidParams, "参数无效");
            }
            var order = new Order();
            Mapper.Map(view, order);

            order.Add(view.UserId, view.Adress, view.Description, order.OrderItems);
            _repo.Insert(order);
            order.OrderItems.ForEach(i => _itemRepo.Insert(i));
        }

        public OrderViewResult Get(string sn)
        {
            var order = _repo.GetBySn(sn);

            OrderViewResult result = new OrderViewResult();
            return order == null ? null : Mapper.Map(order, result);
        }
    }
}
