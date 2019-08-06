using Autofac;
using ChuanGoing.Base.Ioc;

namespace ChuanGoing.SimpleEventBus
{
    public class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
        }
    }
}
