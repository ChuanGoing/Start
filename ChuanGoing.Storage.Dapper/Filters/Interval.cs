using ChuanGoing.Storage.Dapper.Commands;

namespace ChuanGoing.Storage.Dapper.Filters
{
    public class Interval : Filter
    {
        /// <summary>
        /// 区间过滤
        /// </summary>
        /// <param name="field"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Interval(string field, object start, object end) : base(field)
        {
            Start = start;
            End = end;
        }

        public object Start { get; }
        public object End { get; }
    }
}
