using Autofac;
using ChuanGoing.Base.Ioc;

namespace ChuanGoing.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
        }
    }
}
