using ChuanGoing.Base.Data;
using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Storage.Dapper.Commands;
using System;
using System.Data;

namespace ChuanGoing.Storage.Dapper
{
    public class DapperDbContext : IDapperDbContext
    {
        private IDbConnection _connection;
        private readonly IDbBuilder _dbBuilder;

        public IDbTransaction Transaction { get; private set; }

        public ICommandBuilder CommandBuilder { get; private set; }
        /// <summary>
        /// 连接字符串(通过参数注入)
        /// </summary>
        public string ConnectionString { get; private set; }

        public ObjectContextCollection ObjectCollection { get; private set; }

        public DapperDbContext(IDbBuilder dbBuilder, ICommandBuilder commandBuilder, string conStr)
        {
            _dbBuilder = dbBuilder;
            CommandBuilder = commandBuilder;
            ConnectionString = conStr;
            ObjectCollection = new ObjectContextCollection();
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
