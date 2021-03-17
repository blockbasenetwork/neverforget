using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Exceptions;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class PropertyInfoExtensionMethods
    {
        public static bool IsNullable(this PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            return type.GetNullableType() != null;
        }

        public static object[][] GetValues(this PropertyInfo[] properties, object[] objects)
        {
            var values = new List<object[]>();
            foreach (var @object in objects)
            {
                var type = @object.GetType();
                var objectValues = new List<object>();
                foreach (var property in properties)
                {
                    if (type.GetProperty(property.Name) == null)
                    {
                        throw new NoPropertyFoundException(type.Name, property.Name);
                    }

                    var value = property.GetValue(@object);
                    if (value is Guid guidValue && guidValue == Guid.Empty && property.IsNullable())
                    {
                        value = null;
                    }

                    if (value is int intValue && intValue == 0 && property.IsNullable())
                    {
                        value = null;
                    }
                    objectValues.Add(value);
                }
                values.Add(objectValues.ToArray());
            }

            return values.ToArray();
        }

    }
}
