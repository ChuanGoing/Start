using ChuanGoing.Base.Data;
using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Storage.Dapper.Commands;
using System.Data;

namespace ChuanGoing.Storage.Dapper
{
    /// <summary>
    /// Dapper数据库上下文接口
    /// </summary>
    public interface IDapperDbContext : IDbContext
    {
        ObjectContextCollection ObjectCollection { get; }
        ICommandBuilder CommandBuilder { get; }
        IDbTransaction Transaction { get; }
        IDbConnection GetConnection();
        void BeginTransaction(IsolationLevel level);
        void Commit();
        void RollBack();
    }
}
