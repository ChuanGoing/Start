using ChuanGoing.Base.Features;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ChuanGoing.Base.Data
{
    public class Property
    {
        public PropertyInfo Info { get; private set; }
        public IEnumerable<Attribute> Attributes { get; private set; }

        public Property(PropertyInfo propertyInfo)
        {
            Info = propertyInfo;
            Attributes = propertyInfo.GetCustomAttributes();
        }
    }

    public class ObjectContext
    {
        public ObjectContext(Type type)
        {
            Table = GetTableKey(type);
            var properties = new List<Property>();
            foreach (var prop in type.GetProperties())
            {
                properties.Add(new Property(prop));
            }
        }

        public static string GetTableKey(Type type)
        {
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            return tableAttr != null ? tableAttr.Name : type.Name;
        }

        public string Table { get; }
        public IEnumerable<Property> Properties { get; }
        public bool IsEntity { get; }
    }
}
