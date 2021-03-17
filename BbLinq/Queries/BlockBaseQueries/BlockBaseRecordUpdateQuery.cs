using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Queries.Base;
using System;
using System.Collections.Generic;
using BlockBase.BBLinq.Model.Database;
using System.Reflection;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordUpdateQuery : BlockBaseQuery, IUpdateQuery
    {
        public BlockBaseRecordUpdateQuery(Type entityType, object record, ExpressionNode condition, bool isEncrypted) :base(isEncrypted)
        {
            EntityType = entityType;
            Record = record;
            if (condition == null)
            {
                Condition = NodeBuilder.GenerateComparisonNodeOnKey(record);
            }
            Condition = condition;
        }

        public Type EntityType { get; }

        public object Record { get; }

        public ExpressionNode Condition { get; }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            var tableName = EntityType.GetTableName();
            var columns = GetColumnsAndValues();
            queryBuilder.UpdateRecord(tableName, columns, Condition, IsEncrypted);
            return queryBuilder.ToString();
        }

        public (BlockBaseColumn, object)[] GetColumnsAndValues()
        {
            var columns = new List<(BlockBaseColumn, object)>();
            var properties = new List<PropertyInfo>();

            if(EntityType == Record.GetType())
            {
                properties.AddRange(GetFilteredProperties(EntityType, false));
            }
            else
            {
                var unfilteredProperties = Record.GetType().GetProperties();
                var primaryKey = EntityType.GetPrimaryKey();
                foreach (var property in unfilteredProperties)
                {
                    if (property.Name != primaryKey.Name)
                    {
                        properties.Add(property);
                    }
                }
            }

            foreach(var property in properties)
            {
                var value = property.GetValue(Record);
                if(value!= null)
                {
                    columns.Add((BlockBaseColumn.From(property), value));
                }
            }
            return columns.ToArray();
        }
    }
}
