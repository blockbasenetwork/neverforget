using System;
using System.Reflection;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class MemberInfoExtensionMethod
    {
        public static T[] GetAttributes<T>(this MemberInfo property) where T : Attribute
        {
            var attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }
            var attributeList = new T[attributes.Length];
            for (var attributeListCounter = 0; attributeListCounter < attributeList.Length; attributeListCounter++)
            {
                attributeList[attributeListCounter] = attributes[attributeListCounter] as T;
            }
            return attributeList;
        }

        public static T GetAttribute<T>(this MemberInfo property) where T : Attribute
        {
            var attributes = property.GetAttributes<T>();
            return attributes?[0];
        }
    }
}
