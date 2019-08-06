using ChuanGoing.Base.Interface.Ioc;
using System.Data;

namespace ChuanGoing.Base.Interface.Db
{
    public interface IDbBuilder : ISingletonInstance
    {
        IDbConnection CreateConnection(string connStr);
    }
}
