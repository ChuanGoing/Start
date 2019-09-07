using ChuanGoing.Storage.Dapper.Commands;

namespace ChuanGoing.Storage.Dapper.Filters
{
    /// <summary>
    /// 全文索引过滤
    /// </summary>
    public class FullText : Filter
    {
        public FullText(string field, params Word[] words) : base(field)
        {
            Words = words;
        }
        public Word[] Words { get; }
    }

    public class Word
    {
        public Word(string value, WordModifier modifier = WordModifier.Default)
        {
            Modifier = modifier;
            Value = value;
        }
        public WordModifier Modifier { set; get; }
        public string Value { set; get; }
    }

    public enum WordModifier
    {
        Default,
        Essential,
        Without
    }
}
