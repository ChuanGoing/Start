using AutoMapper;
using ChuanGoing.Domain.Modles;
using System;
using System.Collections.Generic;

namespace ChuanGoing.Application.ViewModels
{
    /// <summary>
    /// 订单
    /// </summary>
    [AutoMap(typeof(Order), ReverseMap = true)]
    public class OrderViewModel
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Adress Adress { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
