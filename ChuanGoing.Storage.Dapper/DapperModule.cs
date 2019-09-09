using Autofac;
using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Ioc;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            //
            builder.RegisterGeneric(typeof(DapperRepository<,>)).AsSelf().As(typeof(IRepository<,>)).InstancePerLifetimeScope();
        }
    }
}
