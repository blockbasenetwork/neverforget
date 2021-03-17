using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class TypeExtensionMethods
    {
        public static Type GetNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        public static bool Is<T>(this object obj)
        {
            return obj.GetType().Is(typeof(T));
        }

        public static bool MatchesDataType(this Type type, List<Type> types)
        {
            if (type.IsEnum)
            {
                type = typeof(int);
            }
            return types.Contains(type);
        }

        public static bool Is(this Type type, Type secondType)
        {
            while (type.BaseType != null)
            {
                if (type == secondType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }


        public static bool IsVirtualOrStaticOrAbstract(this PropertyInfo property)
        {
            var getGetMethod = property.GetGetMethod();
            if (getGetMethod == null)
            {
                return false;
            }
            return getGetMethod.IsStatic || getGetMethod.IsVirtual || getGetMethod.IsAbstract;
        }

        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this Type type) where T : Attribute
        {
            var properties = type.GetProperties();
            var propertyList = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                var attributes = property.GetAttributes<T>();
                if (attributes != null && attributes.Length > 0)
                {
                    propertyList.Add(property);
                }
            }
            return propertyList.Count == 0 ? null : propertyList.ToArray();
        }
    }
}
