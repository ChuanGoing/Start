using Autofac;
using ChuanGoing.Base.Ioc;
using ChuanGoing.Storage.Dapper;
using ChuanGoing.Storage.MySql;

namespace ChuanGoing.Storage.Repository
{
    public class RepositoryModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            builder.RegisterModule<DapperModule>();
            builder.RegisterModule<MysqlModule>();
        }
    }
}
