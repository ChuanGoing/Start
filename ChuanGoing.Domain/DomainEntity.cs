using ChuanGoing.Base.Interface.Domain;
using ChuanGoing.Base.Utils;
using System.Reflection;

namespace ChuanGoing.Domain
{
    public abstract class DomainEntity<TPrimaryKey> : IDomainEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; private set; }

        protected DomainEntity()
        {
            Id = GenerateIdHelper.NewId<TPrimaryKey>();
        }

        #region MyRegion

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainEntity<TPrimaryKey>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (DomainEntity<TPrimaryKey>)obj;

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }

        public static bool operator ==(DomainEntity<TPrimaryKey> left, DomainEntity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }
        public static bool operator !=(DomainEntity<TPrimaryKey> left, DomainEntity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
