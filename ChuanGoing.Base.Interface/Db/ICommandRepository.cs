using System.Threading.Tasks;

namespace ChuanGoing.Base.Interface.Db
{
    public interface ICommandRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, ITransactionRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        int Insert(TEntity entity);

        Task<int> InsertAsync(TEntity entity);

        int Update(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        int Delete(TPrimaryKey key);

        Task<int> DeleteAsync(TPrimaryKey key);
    }
}
