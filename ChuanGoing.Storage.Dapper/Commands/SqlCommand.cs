using Dapper;
using System;

namespace ChuanGoing.Storage.Dapper.Commands
{
    public class SqlCommand
    {
        public virtual string SqlString { get; private set; }
        public virtual DynamicParameters Parameters { get; private set; }
        public SqlCommand(string sqlString, DynamicParameters parameters)
        {
            if(string.IsNullOrWhiteSpace(sqlString))
            {
                throw new ArgumentNullException(nameof(sqlString), "invalid sqlString");
            }
            SqlString = sqlString;
            Parameters = parameters;
        }
    }
}
