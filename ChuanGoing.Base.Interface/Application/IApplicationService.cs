using ChuanGoing.Base.Interface.Uow;

namespace ChuanGoing.Base.Interface.Application
{
    public interface IApplicationService
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
