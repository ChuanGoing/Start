using Autofac;
using ChuanGoing.Base.Ioc;
using ChuanGoing.Base.Uow;

namespace ChuanGoing.Base
{
    public class BaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssembly(ThisAssembly);
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
