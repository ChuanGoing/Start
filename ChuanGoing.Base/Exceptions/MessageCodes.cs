namespace ChuanGoing.Base.Exceptions
{
    public class MessageCodes
    {
        #region 公用

        /// <summary>
        /// 成功
        /// </summary>
        public const int Success = 20101000;
        /// <summary>
        /// 警告
        /// </summary>
        public const int Warning = 20102000;
        /// <summary>
        /// 错误
        /// </summary>
        public const int Error = 20103000;
        /// <summary>
        /// 数据验证错误
        /// </summary>
        public const int DataValidationError = 20104000;
        /// <summary>
        /// 数据不存在
        /// </summary>
        public const int DataNotFound = 20105000;
        /// <summary>
        /// 非法的数据状态
        /// </summary>
        public const int IllegalState = 20106000;
        /// <summary>
        /// 参数无效
        /// </summary>
        public const int InvalidParams = 20107000;
        /// <summary>
        /// 输入非法
        /// </summary>
        public const int IllegalInput = 20108000;
        /// <summary>
        /// 鉴权成功
        /// </summary>
        public const int AuthSuccess = 20109000;
        /// <summary>
        /// 无权限访问
        /// </summary>
        public const int AccessDenied = 20110000;

        #endregion

    }
}
