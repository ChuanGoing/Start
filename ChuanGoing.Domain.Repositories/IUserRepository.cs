using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Domain;

namespace ChuanGoing.Domain.Repositories
{
    public interface IUserRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IAggregateRoot<TPrimaryKey>
    {

    }
}
