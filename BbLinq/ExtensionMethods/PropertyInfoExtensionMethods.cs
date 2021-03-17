﻿using System;
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
                    objectValues.Add(property.GetValue(@object));
                }
                values.Add(objectValues.ToArray());
            }

            return values.ToArray();
        }
    }
}
