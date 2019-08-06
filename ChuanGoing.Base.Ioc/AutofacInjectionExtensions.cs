using Autofac;
using ChuanGoing.Base.Interface.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChuanGoing.Base.Ioc
{
    public static class AutofacInjectionExtensions
    {
        /// <summary>
        /// Autofac自动注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        public static void RegisterAssembly(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.IsPublic && !type.IsAbstract && type.IsClass)
                {
                    var interfaces = type.GetInterfaces();
                    IList<Type> transientList = new List<Type>();
                    IList<Type> scopeList = new List<Type>();
                    IList<Type> singletonList = new List<Type>();
                    foreach (var intrType in interfaces)
                    {
                        if (intrType.IsGenericType)
                        {
                            if (intrType.IsAssignableTo<IDependencyInstance>())
                            {
                                transientList.Add(intrType);
                            }
                            else if (intrType.IsAssignableTo<IScopeInstance>())
                            {
                                scopeList.Add(intrType);
                            }
                            else if (intrType.IsAssignableTo<ISingletonInstance>())
                            {
                                singletonList.Add(intrType);
                            }
                        }
                        else
                        {
                            if (intrType.IsAssignableTo<IDependencyInstance>())
                            {
                                transientList.Add(intrType);
                            }
                            else if (intrType.IsAssignableTo<IScopeInstance>())
                            {
                                scopeList.Add(intrType);
                            }
                            else if (intrType.IsAssignableTo<ISingletonInstance>())
                            {
                                singletonList.Add(intrType);
                            }
                        }
                    }

                    if (type.IsGenericType)
                    {
                        if (transientList.Count > 0)
                        {
                            builder.RegisterGeneric(type).As(transientList.ToArray()).InstancePerDependency();
                        }
                        if (scopeList.Count > 0)
                        {
                            builder.RegisterGeneric(type).As(scopeList.ToArray()).InstancePerLifetimeScope();
                        }
                        if (singletonList.Count > 0)
                        {
                            builder.RegisterGeneric(type).As(singletonList.ToArray()).SingleInstance();
                        }

                        //泛型
                        if (type.IsAssignableTo<IDependencyInstance>())
                        {
                            builder.RegisterGeneric(type).AsSelf().InstancePerDependency();
                        }
                        else if (type.IsAssignableTo<IScopeInstance>())
                        {
                            builder.RegisterGeneric(type).AsSelf().InstancePerLifetimeScope();
                        }
                        else if (type.IsAssignableTo<ISingletonInstance>())
                        {
                            builder.RegisterGeneric(type).AsSelf().SingleInstance();
                        }
                    }
                    else
                    {
                        if (transientList.Count > 0)
                        {
                            builder.RegisterType(type).As(transientList.ToArray()).InstancePerDependency();
                        }
                        if (scopeList.Count > 0)
                        {
                            builder.RegisterType(type).As(scopeList.ToArray()).InstancePerLifetimeScope();
                        }
                        if (singletonList.Count > 0)
                        {
                            builder.RegisterType(type).As(singletonList.ToArray()).SingleInstance();
                        }
                        //
                        if (type.IsAssignableTo<IDependencyInstance>())
                        {
                            builder.RegisterType(type).AsSelf().InstancePerDependency();
                        }
                        else if (type.IsAssignableTo<IScopeInstance>())
                        {
                            builder.RegisterType(type).AsSelf().InstancePerLifetimeScope();
                        }
                        else if (type.IsAssignableTo<ISingletonInstance>())
                        {
                            builder.RegisterType(type).AsSelf().SingleInstance();
                        }
                    }
                }
            }
        }
    }
}
