using System.Linq;
using System.Threading.Tasks;

namespace ChuanGoing.Base.Interface.Db
{
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Query

        TEntity Get(TPrimaryKey key);

        Task<TEntity> GetAsync(TPrimaryKey key);

        IQueryable<TEntity> GetAll();

        #endregion

        #region Command

        TPrimaryKey Insert(TEntity entity);

        Task<TPrimaryKey> InsertAsync(TEntity entity);

        int Update(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        int Delete(TPrimaryKey key);

        Task<int> DeleteAsync(TPrimaryKey key);

        #endregion

    }
}
