using ChuanGoing.Storage.Dapper.Commands;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuanGoing.Storage.MySql
{
    public class CommandBuilder : ICommandBuilder
    {
        public CommandBuilder()
        {
        }

        /// <summary>
        /// 检索命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="query"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public SqlCommand QueryCommand(string table, QueryParameter query, int? count)
        {
            if (query.Fields == null)
            {
                throw new ApplicationException("Invalid fields can be checked.");
            }
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            var param = new DynamicParameters();
            var filterStrs = new List<string>();
            var columnStrs = new List<string>();
            var sortStrs = new List<string>();
            if (query.Fields != null)
            {
                foreach (var column in query.Fields)
                {
                    columnStrs.Add($"`{column.Name}`");
                }
            }
            if (query.Filters != null)
            {
                foreach (var filter in query.Filters)
                {
                    var filterCommand = filter.ToSqlCommand();
                    if (filterCommand != null)
                    {
                        filterStrs.Add(filterCommand.SqlString);
                        param.AddDynamicParams(filterCommand.Parameters);
                    }
                }
            }
            if (query.Sorts != null)
            {
                foreach (var sort in query.Sorts)
                {
                    sortStrs.Add($"`{sort.Field}` {(sort.Order ? "ASC" : "DESC")}");
                }
            }
            string strSql = $@"SELECT {(columnStrs.Count > 0 ? string.Join(",", columnStrs) : "*")} FROM `{table}`
                {(filterStrs.Count > 0 ? $" WHERE {string.Join(" AND ", filterStrs)}" : "")}
                {(sortStrs.Count > 0 ? $"ORDER BY {string.Join(",", sortStrs)}" : "")}
                {(count.HasValue ? $" LIMIT {count.Value}" : "")};";
            return new SqlCommand(strSql, param);
        }

        /// <summary>
        /// 插入命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public SqlCommand InsertCommand(string table, FieldsCollection fields)
        {
            if (fields == null || fields.Count == 0)
            {
                throw new ApplicationException("Invalid fields can be inserted.");
            }
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            var param = new DynamicParameters();
            var colNames = new List<string>();
            var prmNames = new List<string>();
            foreach (var field in fields)
            {
                colNames.Add($"`{field.Name}`");
                prmNames.Add($"@{field.Name}");
                param.Add(field.Name, field.Value);
            }
            string strSql = $@"INSERT INTO `{table}`({string.Join(",", colNames)}) VALUES ({string.Join(",", prmNames)});SELECT @@IDENTITY";
            return new SqlCommand(strSql, param);
        }

        /// <summary>
        /// 插入命令(批量)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public SqlCommand InsertBatchCommand(string table, IEnumerable<FieldsCollection> fieldsList)
        {
            if (fieldsList == null || fieldsList.AsList().Count == 0)
            {
                throw new ApplicationException("Invalid fields can be inserted.");
            }
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            var param = new DynamicParameters();
            string strSql = string.Empty;
            var valueStrs = new List<string>();
            var colNames = new List<string>();
            int index = 0;
            foreach (var fields in fieldsList)
            {
                if (fields == null || fields.Count == 0)
                {
                    throw new ApplicationException("Invalid fields can be inserted.");
                }
                var prmNames = new List<string>();
                foreach (var field in fields)
                {
                    if (index == 0)
                    {
                        colNames.Add($"`{field.Name}`");
                    }
                    prmNames.Add($"@{field.Name}_{index}");
                    param.Add($"@{field.Name}_{index}", field.Value);
                }
                valueStrs.Add($@"({string.Join(",", prmNames)})");
                index++;
            }

            strSql = $@"INSERT INTO `{table}`({string.Join(",", colNames)}) VALUES {string.Join(",", valueStrs)};";

            return new SqlCommand(string.Join(";", strSql), param);
        }

        /// <summary>
        /// 更新命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public SqlCommand UpdateCommand(string table, FieldsCollection fields, IEnumerable<Filter> filters)
        {
            if (fields == null || fields.Count == 0)
            {
                throw new ApplicationException("Invalid fields can be update.");
            }
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            var param = new DynamicParameters();
            var columnStrs = new List<string>();
            var filterStrs = new List<string>();
            foreach (var field in fields)
            {
                columnStrs.Add($"`{field.Name}`=@{field.Name}");
                param.Add(field.Name, field.Value);
            }
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    var filterCommand = filter.ToSqlCommand();
                    if (filterCommand != null)
                    {
                        filterStrs.Add(filterCommand.SqlString);
                        param.AddDynamicParams(filterCommand.Parameters);
                    }
                }
            }
            string strSql = $@"UPDATE `{table}` SET {string.Join(",", columnStrs)} 
                {(filterStrs.Count > 0 ? $" WHERE {string.Join(" AND ", filterStrs)}" : "")};";
            return new SqlCommand(strSql, param);
        }


        /// <summary>
        /// 更新命令(批量)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public SqlCommand UpdateCommandBatch(string table, Dictionary<FieldsCollection, IEnumerable<Filter>> dic)
        {
            if (dic == null || dic.Count == 0)
            {
                throw new ApplicationException("Invalid fields can be update.");
            }
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            StringBuilder sb = new StringBuilder();
            var param = new DynamicParameters();
            FieldsCollection fields = null;
            IEnumerable<Filter> filters = null;
            int index = 0;
            foreach (var keyValuePair in dic)
            {
                if (keyValuePair.Key == null || keyValuePair.Key.Count == 0)
                {
                    throw new ApplicationException("Invalid fields can be inserted.");
                }
                if (keyValuePair.Value == null || keyValuePair.Value.AsList().Count == 0)
                {
                    throw new ApplicationException("Invalid fields can be inserted.");
                }
                var columnStrs = new List<string>();
                var filterStrs = new List<string>();

                fields = keyValuePair.Key;
                filters = keyValuePair.Value;

                foreach (var field in fields)
                {
                    columnStrs.Add($"`{field.Name}`=@{field.Name}_{index}");
                    param.Add($"@{field.Name}_{index}", field.Value);
                }
                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        var filterCommand = filter.ToSqlCommand(index);
                        if (filterCommand != null)
                        {
                            filterStrs.Add(filterCommand.SqlString);
                            param.AddDynamicParams(filterCommand.Parameters);
                        }
                    }
                }
                sb.Append($@"UPDATE `{table}` SET {string.Join(",", columnStrs)} 
                {(filterStrs.Count > 0 ? $" WHERE {string.Join(" AND ", filterStrs)}" : "")};");

                index++;
            }

            return new SqlCommand(sb.ToString(), param);
        }


        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public SqlCommand DeleteCommand(string table, IEnumerable<Filter> filters)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ApplicationException("Invalid table's name.");
            }
            var filterStrs = new List<string>();
            var param = new DynamicParameters();
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    var filterCommand = filter.ToSqlCommand();
                    if (filterCommand != null)
                    {
                        filterStrs.Add(filterCommand.SqlString);
                        param.AddDynamicParams(filterCommand.Parameters);
                    }
                }
            }
            string strSql = $"DELETE FROM `{table}` WHERE {string.Join(" AND ", filterStrs)}";
            return new SqlCommand(strSql, param);
        }
    }
}
