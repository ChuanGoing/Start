using ChuanGoing.Base.Domain;
using ChuanGoing.Base.Interface.Domain;

namespace ChuanGoing.Domain
{
    public abstract class DomainEntity<TPrimaryKey> : Entity<TPrimaryKey>, IDomainEntity<TPrimaryKey>
    {

    }
}
