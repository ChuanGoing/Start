using Autofac;
using ChuanGoing.Base.Interface.Db;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperRepository : IRepository
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

        #region Dapper

        protected virtual Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefaultAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        #endregion
    }
}
