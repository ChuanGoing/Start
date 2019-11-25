using System.Data;

namespace ChuanGoing.Base.Interface.Uow
{
    public interface IUnitOfWork
    {
        void Begin(IsolationLevel level = IsolationLevel.Unspecified);
        void SaveChanges();
        void Failed();
    }
}
