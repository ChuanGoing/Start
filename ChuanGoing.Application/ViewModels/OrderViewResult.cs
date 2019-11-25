using AutoMapper;
using ChuanGoing.Domain.Modles;
using System;
using System.Collections.Generic;

namespace ChuanGoing.Application.ViewModels
{
    [AutoMap(typeof(Order), ReverseMap = true)]
    public class OrderViewResult
    {
        /// <summary>
        /// 订单流水号
        /// </summary>        
        public string Sn { get; private set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
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
        public string Adress { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemViewResult> OrderItems { get; set; }
    }
}
