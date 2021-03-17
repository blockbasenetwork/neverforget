using BlockBase.BBLinq.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using BlockBase.BBLinq.Exceptions;

namespace BlockBase.BBLinq.ExtensionMethods
{
    public static class DataAnnotationExtensionMethods
    {
        public static string GetTableName(this Type entity)
        {
            var columnAttribute = entity.GetAttribute<TableAttribute>();
            return columnAttribute == null ? entity.Name : columnAttribute.Name;
        }

        public static string GetColumnName(this PropertyInfo property)
        {
            var columnAttribute = property.GetAttribute<ColumnAttribute>();
            return columnAttribute == null ? property.Name : columnAttribute.Name;
        }

        public static string[] GetColumnNames(this PropertyInfo[] columns)
        {
            var columnNames = new List<string>();
            foreach (var column in columns)
            {
                columnNames.Add(column.GetColumnName());
            }
            return columnNames.ToArray();
        }

        public static bool IsEncryptedValue(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<EncryptedValueAttribute>();
            return attribute != null;
        }

        public static int GetEncryptedValueBuckets(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<EncryptedValueAttribute>();
            return attribute.Buckets;
        }

        public static bool IsDecryptedColumn(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<DecryptedColumnAttribute>();
            return attribute != null;
        }

        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<PrimaryKeyAttribute>();
            return attribute != null;
        }

        public static PropertyInfo GetPrimaryKey(this Type entity)
        {
            var properties = entity.GetPropertiesWithAttribute<PrimaryKeyAttribute>();
            return properties != null && properties.Length > 0 ? properties[0] : null;
        }

        public static ForeignKeyAttribute GetForeignKey(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute;
        }

        public static PropertyInfo[] GetForeignKeys(this Type entity)
        {
            return entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();
        }

        public static PropertyInfo GetForeignKey(this Type entity, Type parentType)
        {
            var foreignKeys = entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();
            foreach(var fk in foreignKeys)
            {
                if(fk.GetForeignKey().Parent == parentType)
                {
                    return fk;
                }
            }
            return null;
        }

        public static Type GetParentType(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute?.Parent;
        }

        public static PropertyInfo GetParentKey(this PropertyInfo property)
        {
            var parent = property.GetParentType();
            return parent == null ? null : parent.GetPrimaryKey();
        }

        public static bool HasDependency(this Type entity, Type parentEntity)
        {
            var foreignKey = entity.GetForeignKey(parentEntity);
            return foreignKey != null;
        }

        public static bool IsDependencyOf(this Type entity, List<Type> sortedEntities)
        {
            foreach (var sortedEntity in sortedEntities)
            {
                if (entity.HasDependency(sortedEntity))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAllDependencies(this List<Type> entities, Type targetEntity)
        {
            var foreignKeys = targetEntity.GetForeignKeys();
            var hasDependencies = true;
            foreach (var foreignKey in foreignKeys)
            {
                if (!entities.Contains(foreignKey.GetForeignKey().Parent))
                {
                    hasDependencies = false;
                }
            }
            return hasDependencies;
        }

        public static bool HasDependencies(this Type entity)
        {
            var foreignKeys = entity.GetPropertiesWithAttribute<ForeignKeyAttribute>();

            return foreignKeys != null && foreignKeys.Length > 0;
        }

        public static bool IsForeignKey(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<ForeignKeyAttribute>();
            return attribute != null;
        }

        public static bool IsMapped(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<MappedAttribute>();
            return attribute != null;
        }

        public static bool IsNotMapped(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<NotMappedAttribute>();
            return attribute != null;
        }

        public static bool IsRange(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RangeAttribute>();
            return attribute != null;
        }

        public static RangeAttribute GetRange(this PropertyInfo property)
        {
            return property.GetAttribute<RangeAttribute>();
        }

        public static bool IsRequired(this PropertyInfo property)
        {
            var attribute = property.GetAttribute<RequiredAttribute>();
            return attribute != null;
        }

        public static Type[] SortByDependency(this Type[] entities)
        {
            var list = new List<Type>(entities);
            var resultList = new List<Type>();
            var hasLooped = false;
            var counter = 0;
            while(list.Count>0)
            {
                var currentEntity = list[counter];
                if (!currentEntity.HasDependencies() || resultList.ContainsAllDependencies(currentEntity))
                {
                    list.Remove(currentEntity);
                    resultList.Add(currentEntity);
                    hasLooped = false;
                }
                else
                {
                    counter++;
                }

                if (counter != list.Count) continue;
                if (hasLooped) throw new NoDependencyFoundInEntitiesException(currentEntity.GetForeignKeys(), resultList.ToArray());
                if (list.Count <= 0) continue;
                hasLooped = true;
                counter = 0;
            }
            return resultList.ToArray();
        }

    }
}
