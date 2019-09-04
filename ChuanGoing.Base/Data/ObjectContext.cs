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

    }
}
