using System.Linq;
using System.Threading.Tasks;

namespace ChuanGoing.Base.Interface.Db
{
    public interface IQueryRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>
    {
        TEntity Get(TPrimaryKey key);

        Task<TEntity> GetAsync(TPrimaryKey key);

        IQueryable<TEntity> GetAll();
    }
}
