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
            builder.RegisterType<DapperDbContext>().As(typeof(IDbContext)).InstancePerLifetimeScope();
            builder.RegisterType<DapperRepository>().As(typeof(IRepository)).InstancePerLifetimeScope();
        }
    }
}
