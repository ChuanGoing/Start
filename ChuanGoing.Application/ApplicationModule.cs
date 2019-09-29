using Autofac;
using ChuanGoing.Base.Ioc;
using ChuanGoing.Domain;
using ChuanGoing.SimpleEventBus;
using ChuanGoing.Storage.MySql;

namespace ChuanGoing.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<EventBusModule>();
            builder.RegisterModule<MysqlModule>();
        }
    }
}
