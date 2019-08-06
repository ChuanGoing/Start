using Autofac;
using ChuanGoing.Base.Ioc;
using ChuanGoing.Storage.Dapper;

namespace ChuanGoing.Storage.MySql
{
    public class MysqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssembly(ThisAssembly);
            builder.RegisterModule<DapperModule>();
        }
    }
}
