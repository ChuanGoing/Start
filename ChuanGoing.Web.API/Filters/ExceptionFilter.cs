using ChuanGoing.Base.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace ChuanGoing.Web.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            #region Ioc/automapper等中间件对错误信息进行了包装,需要解包

            //web错误:验证/鉴权等
            var webException = GetException<Base.Exceptions.WebException>(context.Exception);
            if (webException != null)
            {
                context.Result = new JsonResult(new
                {
                    ErrorCode = webException.ErrorCode ?? MessageCodes.Error,
                    webException.Message
                })
                {
                    StatusCode = (int)webException.HttpStatus
                };
                return;
            }
            //内部错误
            var exception = GetException<InnerException>(context.Exception);
            if (exception != null)
            {
                context.Result = new JsonResult(new
                {
                    ErrorCode = exception.ErrorCode ?? MessageCodes.Error,
                    exception.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return;
            }

            #endregion
        }

        private TException GetException<TException>(Exception exception)
          where TException : Exception
        {
            if (exception == null)
            {
                return null;
            }
            if (exception is TException tException)
            {
                return tException;
            }
            else
            {
                return GetException<TException>(exception.InnerException);
            }
        }
    }
}
