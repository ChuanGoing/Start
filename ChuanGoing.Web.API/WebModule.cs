using Autofac;
using ChuanGoing.Application;
using ChuanGoing.Base.Ioc;

namespace ChuanGoing.Web.API
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            builder.RegisterModule<ApplicationModule>();
        }
    }
}
