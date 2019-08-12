using System;

namespace ChuanGoing.Storage.Dapper.Commands
{
    public class Field
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Field(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "invalid name");
            }
            Name = name;
            Value = value;
        }
    }
}
