using System;
using System.Collections.Generic;
using System.Linq;

namespace ChuanGoing.Domain.Modles
{
    public partial class Order
    {
        public void Add(Guid userId, Adress adress, string description, List<OrderItem> orderItems)
        {
            Sn = Guid.NewGuid().ToString("N");
            TotalPrice = orderItems.Sum(i => i.Price * i.Count);
            Status = OrderStatus.TobePaid;
            ExpireTime = DateTimeOffset.Now.AddMinutes(-30).ToUnixTimeMilliseconds();
            UserId = userId;
            Adress = adress.ToString();
            Description = description;
            orderItems.ForEach(i =>
            {
                i.SetOrder(this);
            });
            SetItems(orderItems);
        }

        public void Pay()
        {
            Status = OrderStatus.Paid;
            PaymentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public void SetItems(List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }
    }
}
