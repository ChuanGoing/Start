using AutoMapper;
using ChuanGoing.Domain.Modles;
using System;

namespace ChuanGoing.Application.ViewModels
{
    [AutoMap(typeof(OrderItem), ReverseMap = true)]
    public class OrderItemViewModel
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
