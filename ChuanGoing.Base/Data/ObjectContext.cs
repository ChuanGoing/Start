using ChuanGoing.Base.Features;
using ChuanGoing.Base.Utils;
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
        public string Table { get; }
        public IEnumerable<Property> Properties { get; }
        public bool IsEntity { get; }

        public static readonly LazyConcurrentDictionary<string, string> _dic = new LazyConcurrentDictionary<string, string>();

        public ObjectContext(Type type)
        {

            Table = GetTableKey(type);
            var properties = new List<Property>();
            foreach (var prop in type.GetProperties())
            {
                properties.Add(new Property(prop));
            }
            Properties = properties;
        }

        public static string GetTableKey(Type type)
        {
            return _dic.GetOrAdd(type.FullName, x => GetKey(type));
        }

        private static string GetKey(Type type)
        {
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            return tableAttr != null ? tableAttr.Name : type.Name;
        }
    }
}
