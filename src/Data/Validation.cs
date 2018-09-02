using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Stellmart.Api.Data
{
    public static class Validation
    {
        public static IEnumerable<string> GetPropertyDiff(object a, object b)
        {
            var returnList = new List<string>();
            var aType = a.GetType();
            var bType = b.GetType();
            if (!aType.Equals(bType))
            {
                throw new InvalidOperationException();
            }
            foreach (var aProperty in aType.GetProperties())
            {
                var aPropertyType = aProperty.PropertyType;
                if (aPropertyType.IsPrimitive || aPropertyType.Equals(typeof(Decimal)) || aPropertyType.Equals(typeof(String)))
                {
                    var aValue = aProperty.GetValue(a, null);
                    var bValue = aProperty.GetValue(b, null);
                    if (!object.Equals(aValue, bValue))
                    {
                        returnList.Add(aProperty.Name);
                    }
                }

            }
            return returnList;
        }
    }
}
