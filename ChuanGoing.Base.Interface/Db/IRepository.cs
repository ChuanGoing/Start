using System.Data;

namespace ChuanGoing.Base.Interface.Db
{
    public interface IRepository
    {
        IDbConnection DbConnection { get; }
    }
}
