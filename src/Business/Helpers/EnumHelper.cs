using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Stellmart.Api.Business.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(false);

            dynamic displayAttribute = null;
            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            return displayAttribute?.Description ?? "Description Not Found";
        }

        public static string GetEnumDescription<TEnum>(string value)
        {
            Type type = typeof(TEnum);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name != null)
            {
                var field = type.GetField(name);

                if (field != null)
                {
                    var attr = type.GetTypeInfo().GetCustomAttribute<DescriptionAttribute>();

                    if (attr != null)
                    {
                        name = attr.Description;
                    }
                }
            }
            return name;
        }

        public static string GetEnumName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string GetEnumDescriptionFromInt<TEnum>(int value)
        {
            var output = Enum.Parse(typeof(TEnum), value.ToString()) as Enum;
            return output.GetEnumDescription();
        }
    }
}
