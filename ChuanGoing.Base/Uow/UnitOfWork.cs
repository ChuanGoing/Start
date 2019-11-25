using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Uow;
using System.Data;

namespace ChuanGoing.Base.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransactionRepository _repository;

        public UnitOfWork(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public virtual void Begin(IsolationLevel level = IsolationLevel.Unspecified)
        {
            _repository.BeginTransaction(level);
        }

        public virtual void SaveChanges()
        {
            _repository.Commit();
        }

        public virtual void Failed()
        {
            _repository.Rollback();
        }
    }
}
