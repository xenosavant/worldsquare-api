using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data
{
    public class Delta<T> : Dictionary<string, dynamic>
    {
        public void Patch(T obj)
        {
            foreach (var key in Keys)
            {
                var type = obj.GetType();
                var property = type.GetProperty(key);
                Type TProperty = property.PropertyType;
                if (property != null)
                {
                    var propertyValue = ChangeType(this[key], TProperty);
                    property.SetValue(obj, propertyValue);
                }
            }
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return null;
                }
                t = Nullable.GetUnderlyingType(t);
            }
            return Convert.ChangeType(value, t);
        }
    }
}
