using Autofac;
using ChuanGoing.Base.Data;
using ChuanGoing.Base.Features;
using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Storage.Dapper.Commands;
using ChuanGoing.Storage.Dapper.Filters;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperRepository<TEntity, TPrimaryKey> :
        ICommandRepository<TEntity, TPrimaryKey>,
        IQueryRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public IDapperDbContext DbContext { get; private set; }
        public IDbConnection DbConnection { get; private set; }
        public ICommandBuilder CommandBuilder { get; private set; }

        public ObjectContextCollection EntitesCollection { get; }

        public DapperRepository(IComponentContext container, IDapperDbContext dapperDbContext)
        {
            DbContext = dapperDbContext;
            CommandBuilder = DbContext.CommandBuilder;
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


        public virtual ObjectContext GetObjectContext<T>()
        {
            var type = typeof(T);

            string tableKey = ObjectContext.GetTableKey(typeof(T));

            return DbContext.ObjectCollection.GetOrAdd(tableKey, entity => new ObjectContext(type));
        }

        #region 

        public virtual TEntity Get(TPrimaryKey key)
        {
            var com = GetCommand(key);
            return QuerySingleOrDefault(com.SqlString, com.Parameters);
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey key)
        {
            var com = GetCommand(key);
            return await QuerySingleOrDefaultAsync(com.SqlString, com.Parameters);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            var entity = GetObjectContext<TEntity>();
            FieldsCollection columns = new FieldsCollection
            {
                entity.Properties.Select(e=>new Field(e.Info.Name))
            };
            QueryParameter queryParameter = new QueryParameter(columns);
            SqlCommand command = CommandBuilder.QueryCommand(entity.Table, queryParameter);
            return Query<TEntity>(command.SqlString, command.Parameters).AsQueryable();
        }

        public int Insert(TEntity entity)
        {
            var com = InsertCommand(entity);
            return Execute(com.SqlString, com.Parameters);
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var com = InsertCommand(entity);
            return await ExecuteAsync(com.SqlString, com.Parameters);
        }

        public virtual int Update(TEntity entity)
        {
            var com = UpdateCommand(entity);
            return Execute(com.SqlString, com.Parameters);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            var com = UpdateCommand(entity);
            return await ExecuteAsync(com.SqlString, com.Parameters);
        }

        public virtual int Delete(TPrimaryKey key)
        {
            var com = DeleteCommand(new List<TPrimaryKey> { key });
            return Execute(com.SqlString, com.Parameters);
        }

        public virtual async Task<int> DeleteAsync(TPrimaryKey key)
        {
            var com = DeleteCommand(new List<TPrimaryKey> { key });
            return await ExecuteAsync(com.SqlString, com.Parameters);
        }

        public virtual int Delete(IEnumerable<TPrimaryKey> keys)
        {
            var com = DeleteCommand(keys);
            return Execute(com.SqlString, com.Parameters);
        }

        public virtual async Task<int> DeleteAsync(IEnumerable<TPrimaryKey> keys)
        {
            var com = DeleteCommand(keys);
            return await ExecuteAsync(com.SqlString, com.Parameters);
        }

        #endregion

        #region Command

        public SqlCommand GetCommand(TPrimaryKey key)
        {
            var obj = GetObjectContext<TEntity>();
            FieldsCollection fields = new FieldsCollection();
            List<Filter> filters = new List<Filter>();
            foreach (var prop in obj.Properties)
            {
                foreach (var attr in prop.Attributes)
                {
                    if (attr is PrimaryKeyAttribute keyAttr)
                    {
                        filters.Add(new Equal(prop.Info.Name, key));
                    }
                }

                fields.Add(new Field(prop.Info.Name));
            }

            QueryParameter queryParameter = new QueryParameter(fields, filters);
            return CommandBuilder.QueryCommand(obj.Table, queryParameter, count: 1);
        }

        public SqlCommand InsertCommand(TEntity entity)
        {
            var obj = GetObjectContext<TEntity>();
            FieldsCollection fields = new FieldsCollection();
            bool skip = false;
            foreach (var prop in obj.Properties)
            {
                skip = false;
                foreach (var attr in prop.Attributes)
                {
                    if (attr is IgnoreAttribute)
                    {
                        skip = true;
                    }
                }
                if (!skip) fields.Add(new Field(prop.Info.Name, prop.Info.GetValue(entity)));
            }
            var com = CommandBuilder.InsertCommand(obj.Table, fields);
            return com;
        }

        public SqlCommand UpdateCommand(TEntity entity)
        {
            var obj = GetObjectContext<TEntity>();
            FieldsCollection fields = new FieldsCollection();
            List<Filter> filters = new List<Filter>();
            foreach (var prop in obj.Properties)
            {
                var value = prop.Info.GetValue(entity);
                bool isContinue = true;
                foreach (var attr in prop.Attributes)
                {
                    if (attr is PrimaryKeyAttribute keyAttr)
                    {
                        filters.Add(new Equal(prop.Info.Name, value));
                        isContinue = false;
                    }
                }
                if (!isContinue) continue;
                fields.Add(new Field(prop.Info.Name, value));
            }
            return CommandBuilder.UpdateCommand(obj.Table, fields, filters);
        }

        public SqlCommand DeleteCommand(IEnumerable<TPrimaryKey> keys)
        {
            var obj = GetObjectContext<TEntity>();
            List<Filter> filters = new List<Filter>();
            foreach (var prop in obj.Properties)
            {
                foreach (var attr in prop.Attributes)
                {
                    if (attr is PrimaryKeyAttribute keyAttr)
                    {
                        filters.Add(new Equal(prop.Info.Name, keys));
                        break;
                    }
                }
            }
            return CommandBuilder.DeleteCommand(obj.Table, filters);
        }
        #endregion


        #region Dapper

        protected virtual int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Execute(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteAsync(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual IDataReader ExecuteReader(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteReader(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteReaderAsync(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual IDataReader ExecuteReader(string sql, CommandBehavior commandBehavior, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteReader(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<IDataReader> ExecuteReaderAsync(string sql, CommandBehavior commandBehavior, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteReaderAsync(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TResult ExecuteScalar<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteScalar<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteScalarAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TEntity ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteScalar<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TEntity> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteScalarAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual IEnumerable<TEntity> Query(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Query<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual IEnumerable<TResult> Query<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Query<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TEntity QueryFirst(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirst<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TEntity> QueryFirstAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TResult QueryFirst<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirst<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TResult> QueryFirstAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TEntity QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefault<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TResult QueryFirstOrDefault<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefault<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefaultAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefaultAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TEntity QuerySingle(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingle<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TEntity> QuerySingleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TResult QuerySingle<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingle<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TResult> QuerySingleAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TEntity QuerySingleOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleOrDefault<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleOrDefaultAsync<TEntity>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual TResult QuerySingleOrDefault<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleOrDefault<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<TResult> QuerySingleOrDefaultAsync<TResult>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QuerySingleOrDefaultAsync<TResult>(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual SqlMapper.GridReader QueryMultiple(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryMultiple(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        protected virtual Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryMultipleAsync(sql: sql, param: param, transaction: DbContext.Transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        #endregion
    }
}
