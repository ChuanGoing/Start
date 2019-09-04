namespace ChuanGoing.Base.Interface.Db
{
    public interface IEntity
    {
    }

    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
    }
}
