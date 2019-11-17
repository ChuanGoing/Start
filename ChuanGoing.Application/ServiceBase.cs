using Autofac;
using AutoMapper;
using ChuanGoing.Base.Interface.Application;
using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Uow;

namespace ChuanGoing.Application
{
    public class ServiceBase<TEntity, TPrimaryKey> : IApplicationService
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected IMapper Mapper { get; private set; }
        public virtual IUnitOfWork UnitOfWork { get; private set; }

        public ServiceBase(IComponentContext container, ICommandRepository<TEntity, TPrimaryKey> repository)
        {
            Mapper = container.Resolve<IMapper>();
            UnitOfWork = container.Resolve<IUnitOfWork>(new TypedParameter(typeof(ITransactionRepository), repository));
        }
    }
}
