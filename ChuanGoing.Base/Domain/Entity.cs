using ChuanGoing.Base.Features;
using ChuanGoing.Base.Interface.Db;
using System.Reflection;

namespace ChuanGoing.Base.Domain
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [PrimaryKey]
        public virtual TPrimaryKey Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Entity<TPrimaryKey>)obj;

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

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }
        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }
    }
}
