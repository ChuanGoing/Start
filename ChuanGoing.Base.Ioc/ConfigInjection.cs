using Autofac;

namespace ChuanGoing.Base.Ioc
{
    public class ConfigInjection
    {
        public readonly ContainerBuilder Container;
        public ConfigInjection(ContainerBuilder container)
        {
            Container = container;
        }

        public static ConfigInjection Default(ContainerBuilder container)
        {
            return new ConfigInjection(container);
        }

        #region 配置信息注入

        /// <summary>
        /// DbContext连接字符串注入
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public ConfigInjection UseDbContext<TDbContext, TService>(string connStr)
        {
            Container.RegisterType<TDbContext>()
                .WithProperty("ConnectionString", connStr)
                .Named<TService>(typeof(TDbContext).FullName)
                .InstancePerLifetimeScope();
            return this;
        }

        #endregion

    }
}
