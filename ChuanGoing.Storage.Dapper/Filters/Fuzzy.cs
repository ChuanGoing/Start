using ChuanGoing.Storage.Dapper.Commands;

namespace ChuanGoing.Storage.Dapper.Filters
{
    public enum FuzzyType
    {
        Left,
        Right,
        All
    }

    /// <summary>
    /// 模糊过滤
    /// </summary>
    public class Fuzzy : Filter
    {
        public Fuzzy(string field, object value, FuzzyType type = FuzzyType.Right) : base(field)
        {
            Value = value;
            Type = type;
        }
        public object Value { get; }
        public FuzzyType Type { get; }
    }
}
