using ChuanGoing.Base.Interface.Ioc;
using System.Collections.Generic;

namespace ChuanGoing.Storage.Dapper.Commands
{
    public interface ICommandBuilder : ISingletonInstance
    {
        /// <summary>
        /// 检索命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Query"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        SqlCommand QueryCommand(string table, QueryParameter parameter, int? count = null);

        /// <summary>
        /// 插入命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        SqlCommand InsertCommand(string table, FieldsCollection fields);

        /// <summary>
        /// 插入命令(批量)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        SqlCommand InsertBatchCommand(string table, IEnumerable<FieldsCollection> fieldsList);

        /// <summary>
        /// 更新命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        SqlCommand UpdateCommand(string table, FieldsCollection fields, IEnumerable<Filter> filters);

        /// <summary>
        /// 更新命令(批量)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        SqlCommand UpdateCommandBatch(string table, Dictionary<FieldsCollection, IEnumerable<Filter>> dic);

        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="table"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        SqlCommand DeleteCommand(string table, IEnumerable<Filter> filters);
    }
}
