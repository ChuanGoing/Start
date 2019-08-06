using System.Data;

namespace ChuanGoing.Base.Interface.Db
{
    /// <summary>
    /// Dapper数据库上下文接口
    /// </summary>
    public interface IDapperDbContext : IDbContext
    {
        IDbTransaction Transaction { get; }
        IDbConnection GetConnection();
        void BeginTransaction(IsolationLevel level);
        void Commit();
        void RollBack();
    }
}
