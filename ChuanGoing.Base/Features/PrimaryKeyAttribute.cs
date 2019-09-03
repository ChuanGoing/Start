using System;

namespace ChuanGoing.Base.Features
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PrimaryKeyAttribute : Attribute
    {
        public bool AutoIncrement { get; set; } = false;
    }
}
