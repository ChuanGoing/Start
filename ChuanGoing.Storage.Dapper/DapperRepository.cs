using Autofac;
using ChuanGoing.Base.Interface.Db;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public IDapperDbContext DbContext { get; private set; }
        public IDbConnection DbConnection { get; private set; }

        public DapperRepository(IComponentContext container)
        {
            DbContext = container.ResolveNamed<IDapperDbContext>(typeof(DapperDbContext).FullName);
            DbConnection = DbContext.GetConnection();
        }

        public virtual void BeginTransaction(IsolationLevel level = IsolationLevel.Unspecified)
        {
            DbContext.BeginTransaction(level);
        }

        public virtual void Commit()
        {
            DbContext.Commit();
        }

        public virtual void Rollback()
        {
            DbContext.RollBack();
        }




        #region 

        public TEntity Get(TPrimaryKey key)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetAsync(TPrimaryKey key)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public TPrimaryKey Insert(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TPrimaryKey> InsertAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public int Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(TPrimaryKey key)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(TPrimaryKey key)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
