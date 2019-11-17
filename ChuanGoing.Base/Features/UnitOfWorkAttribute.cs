using AspectCore.DynamicProxy;
using ChuanGoing.Base.Interface.Application;
using System.Threading.Tasks;

namespace ChuanGoing.Base.Features
{
    /// <summary>
    /// Uow 切面实现事务
    /// </summary>
    public class UnitOfWorkAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (context.Implementation is IApplicationService applicationService)
            {
                var uow = applicationService.UnitOfWork;
                uow.Begin();
                var aspectDelegate = next(context);
                if (aspectDelegate.Exception != null)
                {
                    uow.Failed();
                    throw aspectDelegate.Exception;
                }
                else
                {
                    uow.SaveChanges();
                    return aspectDelegate;
                }
            }
            else
            {
                return next(context);
            }
        }
    }
}
