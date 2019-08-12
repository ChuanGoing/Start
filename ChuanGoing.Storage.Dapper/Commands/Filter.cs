namespace ChuanGoing.Storage.Dapper.Commands
{
    /// <summary>
    /// 抽象字段过滤器
    /// </summary>
    public abstract class Filter
    {
        public virtual string Field { get; private set; }
        public Filter(string field)
        {
            Field = field;
        }
    }
}
