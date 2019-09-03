using System;

namespace ChuanGoing.Base.Features
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : Attribute
    {
        public string Name { get; private set; }
        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
