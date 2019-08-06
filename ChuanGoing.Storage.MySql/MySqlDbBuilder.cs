using ChuanGoing.Base.Interface.Db;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChuanGoing.Storage.MySql
{
    public class MySqlDbBuilder : IDbBuilder
    {
        public MySqlDbBuilder()
        {
        }

        public IDbConnection CreateConnection(string connStr)
        {
            return new MySqlConnection(connStr);
        }
    }
}
