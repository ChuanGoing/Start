using Autofac;
using ChuanGoing.Base;
using ChuanGoing.Base.Ioc;

namespace ChuanGoing.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            builder.RegisterModule<BaseModule>();
        }
    }
}
