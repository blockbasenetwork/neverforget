﻿using System;
using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Database
{
    public class BlockBaseColumn : DatabaseColumn
    {
        public PropertyInfo Property { get; set; }
        public bool IsColumnDecrypted { get; set; }
        public bool IsValueEncrypted { get; set; }
        public int BucketCount { get; set; }
        public bool IsRange { get; set; }
        public int RangeMinimum { get; set; }
        public BlockBaseDataTypeEnum DataType { get; set; }
        public int RangeMaximum { get; set; }

        public static BlockBaseColumn From(PropertyInfo property)
        {
            var propertyType = property.IsNullable() ? property.PropertyType.GetNullableType() : property.PropertyType;

            var column = new BlockBaseColumn
            {
                Name = property.GetColumnName(),
                Table = property.ReflectedType.GetTableName(),
                IsPrimaryKey = property.IsPrimaryKey(),
                IsColumnDecrypted = property.IsDecryptedColumn(),
                IsValueEncrypted = property.IsEncryptedValue(),
                IsRange = property.IsRange(),
                IsForeignKey = property.IsForeignKey(),
                IsRequired = property.IsRequired(),
                Property = property
            };

            if (column.IsRange)
            {
                var range = property.GetRange();
                column.BucketCount = range.Buckets;
                column.RangeMaximum = range.Maximum;
                column.RangeMinimum = range.Minimum;
            }

            if (column.IsValueEncrypted)
            {
                column.BucketCount = property.GetEncryptedValueBuckets();
            }

            if (column.IsForeignKey)
            {
                column.ParentTableName = property.GetParentType().GetTableName();
                column.ParentTableKeyName = property.GetParentKey().GetColumnName();
            }

            column.DataType = column.IsValueEncrypted ? BlockBaseDataTypeEnum.Encrypted : propertyType.ToBbSqlType();
            return column;
        }
    }

}
