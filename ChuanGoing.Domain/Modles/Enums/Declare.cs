namespace ChuanGoing.Domain.Modles
{
    public enum OrderStatus
    {
        /// <summary>
        /// 待支付
        /// </summary>
        TobePaid = 1,
        /// <summary>
        /// 取消订单
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// 关闭
        /// </summary>
        Close = 3,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 4
    }
}
