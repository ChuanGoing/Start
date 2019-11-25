using System;

namespace ChuanGoing.Base.Exceptions
{
    /// <summary>
    /// 自定义错误信息
    /// </summary>
    public class InnerException : Exception
    {
        /// <summary>
        /// 内部错误代码
        /// </summary>
        public int? ErrorCode { get; }

        public InnerException(int errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public InnerException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public InnerException(int code, string message, Exception exception) : base(message, exception)
        {
            ErrorCode = code;
        }
    }
}
