namespace ChuanGoing.Storage.Dapper.Commands
{
    public class Sort
    {
        public string Field { get; set; }
        public bool Order { get; set; }

        public Sort(string field, bool order = true)
        {
            Field = field;
            Order = order;
        }
    }
}
