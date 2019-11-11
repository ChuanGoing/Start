using Autofac;
using AutoMapper;

namespace ChuanGoing.Application
{
    public class ServiceBase
    {
        protected IMapper Mapper { get; private set; }

        public ServiceBase(IComponentContext container)
        {
            Mapper = container.Resolve<IMapper>();
        }
    }
}
