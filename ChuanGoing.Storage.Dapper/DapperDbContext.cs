using ChuanGoing.Base.Interface.Db;
using System;
using System.Data;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperDbContext : IDapperDbContext
    {
        private IDbConnection _connection;
        private readonly IDbBuilder _dbBuilder;

        public IDbTransaction Transaction { get; private set; }
        /// <summary>
        /// 连接字符串(通过属性注入)
        /// </summary>
        public string ConnectionString { get; set; }

        public DapperDbContext(IDbBuilder dbBuilder)
        {
            _dbBuilder = dbBuilder;
        }

        /// <summary>
        /// 获取db连接对象
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            if (_connection == null && _dbBuilder != null)
            {
                _connection = _dbBuilder.CreateConnection(ConnectionString);
            }
            return _connection;
        }

        #region 事物处理

        public void BeginTransaction(IsolationLevel level)
        {
            OpenConnection();
            if (Transaction == null)
            {
                Transaction = _connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_connection.State == ConnectionState.Open)
            {
                Transaction.Commit();
                CloseConnection();
                Transaction = null;
            }
        }

        public void RollBack()
        {
            if (_connection.State == ConnectionState.Open)
            {
                Transaction.Rollback();
                CloseConnection();
                Transaction = null;
            }
        }

        private void OpenConnection()
        {
            if (_connection == null)
            {
                throw new Exception("没有数据库连接对象");
            }
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection == null)
            {
                throw new Exception("没有数据库连接对象");
            }
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        #endregion

    }
}
