using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChuanGoing.Base.Ioc
{
    /// <summary>
    /// Autofac Ioc容器替换.NET 的Ioc
    /// </summary>
    public static class AutofacExtensions
    {
        public static IServiceProvider UseAutofac<TModule>(this IServiceCollection services, Action<ConfigInjection> config = null)
           where TModule : Module, new()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);

            ConfigInjection defaultConfig = ConfigInjection.Default(builder);
            if (config != null)
            {
                config.Invoke(defaultConfig);
            }

            builder.RegisterModule<TModule>();
            builder.RegisterDynamicProxy();
            IContainer container = builder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
