using System;
using System.Reflection;

namespace Lob.Net.Helpers
{
    internal static class EnumExtensions
    {
        public static string GetValue(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    ValueAttribute attr = Attribute.GetCustomAttribute(field, typeof(ValueAttribute)) as ValueAttribute;
                    if (attr != null)
                    {
                        return attr.Name;
                    }
                }
            }

            return null;
        }
    }
}
