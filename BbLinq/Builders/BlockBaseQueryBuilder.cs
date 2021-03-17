﻿using System;
using System.Globalization;
using System.Reflection;
using BlockBase.BBLinq.Builders.Base;
using BlockBase.BBLinq.Dictionaries;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;

namespace BlockBase.BBLinq.Builders
{
    public class BlockBaseQueryBuilder : QueryBuilder<BlockBaseQueryBuilder, BbSqlDictionary>
    {
        public BlockBaseQueryBuilder()
        {
            Dictionary = new BbSqlDictionary();
        }

        #region SimpleExpressions

        public BlockBaseQueryBuilder Use()
        {
            return Append(Dictionary.Use);
        }

        public BlockBaseQueryBuilder Create()
        {
            return Append(Dictionary.Create);
        }

        public BlockBaseQueryBuilder End()
        {
            return Append(Dictionary.QueryEnd);
        }

        public BlockBaseQueryBuilder Drop()
        {
            return Append(Dictionary.Drop);
        }

        public BlockBaseQueryBuilder Database()
        {
            return Append(Dictionary.DataBase);
        }

        public BlockBaseQueryBuilder Delete()
        {
            return Append(Dictionary.Delete);
        }

        public BlockBaseQueryBuilder From()
        {
            return Append(Dictionary.From);
        }

        public BlockBaseQueryBuilder WhiteSpace()
        {
            return Append(Dictionary.WhiteSpace);
        }

        public BlockBaseQueryBuilder TableColumnSeparator()
        {
            return Append(Dictionary.TableColumnSeparator);
        }

        public BlockBaseQueryBuilder If()
        {
            return Append(Dictionary.If);
        }

        public BlockBaseQueryBuilder Execute()
        {
            return Append(Dictionary.Execute);
        }

        public BlockBaseQueryBuilder QueryBatchLeftWrapper()
        {
            return Append(Dictionary.QueryBatchWrapperLeft);
        }

        public BlockBaseQueryBuilder QueryBatchRightWrapper()
        {
            return Append(Dictionary.QueryBatchWrapperRight);
        }

        public BlockBaseQueryBuilder Where()
        {
            return Append(Dictionary.Where);
        }

        public BlockBaseQueryBuilder EqualTo()
        {
            return Append(Dictionary.ValueEquals);
        }

        public BlockBaseQueryBuilder Insert()
        {
            return Append(Dictionary.Insert);
        }

        public BlockBaseQueryBuilder Into()
        {
            return Append(Dictionary.Into);
        }

        public BlockBaseQueryBuilder ListSeparator()
        {
            return Append(Dictionary.ColumnSeparator);
        }

        public BlockBaseQueryBuilder Values()
        {
            return Append(Dictionary.Values);
        }

        public BlockBaseQueryBuilder WrapListLeft()
        {
            return Append(Dictionary.LeftListWrapper);
        }

        public BlockBaseQueryBuilder WrapListRight()
        {
            return Append(Dictionary.RightListWrapper);
        }

        public BlockBaseQueryBuilder Update()
        {
            return Append(Dictionary.Update);
        }

        public BlockBaseQueryBuilder Set()
        {
            return Append(Dictionary.Set);
        }

        public BlockBaseQueryBuilder NotNull()
        {
            return Append(Dictionary.NotNull);
        }

        public BlockBaseQueryBuilder Select()
        {
            return Append(Dictionary.Select);
        }

        public BlockBaseQueryBuilder Encrypted()
        {
            return Append(Dictionary.Encrypted);
        }

        public BlockBaseQueryBuilder Limit()
        {
            return Append(Dictionary.Limit);
        }

        public BlockBaseQueryBuilder Offset()
        {
            return Append(Dictionary.Offset);
        }

        public BlockBaseQueryBuilder Begin()
        {
            return Append(Dictionary.Begin);
        }

        public BlockBaseQueryBuilder Commit()
        {
            return Append(Dictionary.Commit);
        }

        public BlockBaseQueryBuilder Transaction()
        {
            return Append(Dictionary.Transaction);
        }

        public BlockBaseQueryBuilder Rollback()
        {
            return Append(Dictionary.Rollback);
        }

        public BlockBaseQueryBuilder Table()
        {
            return Append(Dictionary.Table);
        }

        public BlockBaseQueryBuilder NonEncryptedColumn()
        {
            return Append(Dictionary.NonEncryptedColumn);
        }

        public BlockBaseQueryBuilder Join()
        {
            return Append(Dictionary.Join);
        }

        public BlockBaseQueryBuilder On()
        {
            return Append(Dictionary.On);
        }

        public BlockBaseQueryBuilder Range()
        {
            return Append(Dictionary.Range);
        }

        public BlockBaseQueryBuilder PrimaryKey()
        {
            return Append(Dictionary.PrimaryKey);
        }

        public BlockBaseQueryBuilder References()
        {
            return Append(Dictionary.References);
        }

        #endregion

        #region Support

        #region Use Database

        public BlockBaseQueryBuilder UseDatabase(string databaseName)
        {
            return Use().WhiteSpace().Append(databaseName).End();
        }

        #endregion

        #region Transaction

        public BlockBaseQueryBuilder BeginTransaction()
        {
            return Begin().WhiteSpace().Transaction().End();
        }

        public BlockBaseQueryBuilder RollbackTransaction()
        {
            return Rollback().WhiteSpace().Transaction().End();
        }

        public BlockBaseQueryBuilder CommitTransaction()
        {
            return Commit().WhiteSpace().Transaction().End();
        }


        #endregion

        #endregion

        #region Structure queries

        #region Create Database

        public BlockBaseQueryBuilder CreateDatabase(string databaseName, bool isEncrypted)
        {
            return Create().WhiteSpace().Database().WhiteSpace().Append(databaseName).EncryptQuery(isEncrypted).End();
        }

        #endregion

        #region Delete Database

        public BlockBaseQueryBuilder DropDatabase(string databaseName, bool isEncrypted)
        {
            return Drop().WhiteSpace().Database().WhiteSpace().Append(databaseName).EncryptQuery(isEncrypted).End();
        }

        #endregion

        #region Create Table

        public BlockBaseQueryBuilder CreateTable(string tableName, BlockBaseColumn[] columns)
        {
            Create().WhiteSpace().Table().WhiteSpace().Append(tableName).WhiteSpace();
            ListColumns(columns, true, CreateColumn);
            return End();
        }

        #endregion

        #endregion

        #region Record queries

        #region Insert

        public BlockBaseQueryBuilder InsertRecord(string tableName, BlockBaseColumn[] columns, object[][] values,
            bool isEncrypted)
        {
            Insert().WhiteSpace().Into().WhiteSpace().Append(tableName).WhiteSpace();
            ListColumns(columns, true, c => Append(c.Name));
            WhiteSpace().Values().WhiteSpace();
            foreach (var record in values)
            {
                WrapListLeft();
                foreach (var value in record)
                {
                    WrapValue(value);
                    if (value != record[^1])
                    {
                        ListSeparator().WhiteSpace();
                    }
                }
                WrapListRight();
                if (record != values[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            return EncryptQuery(isEncrypted).End();
        }

        #endregion

        #region Update

        public BlockBaseQueryBuilder UpdateRecord(string tableName, (BlockBaseColumn, object)[] columns,
            ExpressionNode condition, bool isEncrypted)
        {
            var last = columns[^1].Item1.Name;
            Update().WhiteSpace().Append(tableName).WhiteSpace().Set().WhiteSpace();

            foreach (var (column, value) in columns)
            {

                Append(column.Name).WhiteSpace().EqualTo().WhiteSpace().WrapValue(value);
                if (column.Name != last)
                {
                    ListSeparator().WhiteSpace();
                }
            }

            return Where(condition).EncryptQuery(isEncrypted).End();
        }

        #endregion

        #region Delete

        public BlockBaseQueryBuilder DeleteRecord(string tableName, ExpressionNode condition, bool isEncrypted)
        {
            Delete().WhiteSpace()
                .From().WhiteSpace().Append(tableName)
                .Where(condition)
                .EncryptQuery(isEncrypted);
            return End();
        }

        #endregion

        #region Select

        public BlockBaseQueryBuilder SelectRecord(BlockBaseColumn[] columns, JoinNode[] joins, ExpressionNode condition,
            int? limit, int? offset, bool isEncrypted)
        {
            Select().WhiteSpace().ListColumns(columns, false, Column).WhiteSpace();
            for (var joinIndex = 0; joinIndex < joins.Length; joinIndex++)
            {
                var join = joins[joinIndex];
                if (joinIndex == 0)
                {
                    From().WhiteSpace().Table(join.Left.Property);
                }
                if(join.Right.Property != null)
                {
                    ParseJoinNode(join);
                }
            }
            Where(condition);
            Limit(limit, offset);
            return End();
        }

    #endregion

        #endregion

        #region Auxiliary

        public BlockBaseQueryBuilder Limit(int? limit, int? offset)
        {
            if (limit == null) return this;
            Limit().WhiteSpace().Append(limit.ToString()).WhiteSpace();
            if (offset != null)
            {
                Offset().WhiteSpace().Append(offset.ToString()).WhiteSpace();
            }

            return this;
        }
        public BlockBaseQueryBuilder ListColumns(BlockBaseColumn[] columns, bool wrap, Func<BlockBaseColumn, BlockBaseQueryBuilder> actionOnColumn)
        {
            if (wrap)
            {
                WrapListLeft();
            }
            foreach (var column in columns)
            {
                actionOnColumn(column);
                if (column != columns[^1])
                {
                    ListSeparator().WhiteSpace();
                }
            }
            if (wrap)
            {
                WrapListRight();
            }

            return this;
        }


        public BlockBaseQueryBuilder Where(ExpressionNode condition)
        {
            if (condition == null) return this;
            WhiteSpace().Where().WhiteSpace();
            ParseNode(condition);
            return this;
        }

        private BlockBaseQueryBuilder CreateColumn(BlockBaseColumn column)
        {
            if (column.IsColumnDecrypted)
            {
                Append(Dictionary.NonEncryptedColumn);
            }
            Append(column.Name).WhiteSpace();

            if (column.IsValueEncrypted)
            {
                Encrypted().WhiteSpace().Append(column.BucketCount.ToString());
            }
            else if (column.IsRange)
            {
                Encrypted().WhiteSpace().Range().WrapListLeft()
                    .Append(column.BucketCount.ToString()).ListSeparator().WhiteSpace()
                    .Append(column.RangeMinimum.ToString()).ListSeparator().WhiteSpace()
                    .Append(column.RangeMaximum.ToString()).WrapListRight();
            }
            else
            {
                Append(column.DataType.ToString());
            }

            if (column.IsRequired && !column.IsPrimaryKey)
            {
                WhiteSpace().NotNull();
            }

            if (column.IsPrimaryKey)
            {
                WhiteSpace().PrimaryKey();
            }

            if (column.IsForeignKey)
            {
                WhiteSpace().References().WhiteSpace().Append(column.ParentTableName).WrapListLeft()
                    .Append(column.ParentTableKeyName).WrapListRight();
            }

            return this;
        }

        public BlockBaseQueryBuilder EncryptQuery(bool isEncrypted)
        {
            if (isEncrypted)
            {
                Encrypted();
            }

            return this;
        }

        #region Node parsing

        private BlockBaseQueryBuilder ParseNode(ExpressionNode node)
        {
            return node switch
            {
                LogicNode logicNode => ParseLogicNode(logicNode),
                ComparisonNode comparisonNode => ParseComparisonNode(comparisonNode),
                JoinNode joinNode => ParseJoinNode(joinNode),
                PropertyNode propertyNode => ParsePropertyNode(propertyNode),
                ValueNode valueNode => ParseValueNode(valueNode),
                _ => this
            };
        }

        private BlockBaseQueryBuilder ParseLogicNode(LogicNode node)
        {
            ParseNode(node.Left).WhiteSpace()
                .ParseOperator(node.Operator).WhiteSpace()
                .ParseNode(node.Right);
            return this;
        }

        private BlockBaseQueryBuilder ParseComparisonNode(ComparisonNode node)
        {
            ParseNode(node.Left).WhiteSpace()
                .ParseOperator(node.Operator)
                .ParseNode(node.Right);
            return this;
        }

        private BlockBaseQueryBuilder ParseJoinNode(JoinNode node)
        {
            var rightTable = node.Right.Property.ReflectedType.GetTableName();
                WhiteSpace().Join().WhiteSpace().Append(rightTable)
                .WhiteSpace().On().WhiteSpace()
                .ParsePropertyNode(node.Left).WhiteSpace()
                .WhiteSpace().EqualTo().WhiteSpace()
                .ParsePropertyNode(node.Right);
            return this;
        }

        private BlockBaseQueryBuilder ParsePropertyNode(PropertyNode node)
        {
            var table = node.Property.ReflectedType.GetTableName();
            var column = node.Property.GetColumnName();
            Append(table).TableColumnSeparator().Append(column);
            return this;
        }

        private BlockBaseQueryBuilder ParseValueNode(ValueNode node)
        {
            WrapValue(node.Value);
            return this;
        }

        private BlockBaseQueryBuilder ParseOperator(BlockBaseComparisonOperator @operator)
        {
            switch (@operator)
            {
                case BlockBaseComparisonOperator.EqualTo:
                    Append(Dictionary.ValueEquals);
                    break;
                case BlockBaseComparisonOperator.DifferentFrom:
                    Append(Dictionary.DifferentFrom);
                    break;
                case BlockBaseComparisonOperator.GreaterOrEqualTo:
                    Append(Dictionary.EqualOrGreaterThan);
                    break;
                case BlockBaseComparisonOperator.GreaterThan:
                    Append(Dictionary.GreaterThan);
                    break;
                case BlockBaseComparisonOperator.LessOrEqualTo:
                    Append(Dictionary.EqualOrLessThan);
                    break;
                case BlockBaseComparisonOperator.LessThan:
                    Append(Dictionary.LessThan);
                    break;
            }
            return this;
        }

        private BlockBaseQueryBuilder ParseOperator(BlockBaseLogicOperator @operator)
        {
            switch (@operator)
            {
                case BlockBaseLogicOperator.And:
                    Append(Dictionary.And);
                    break;
                case BlockBaseLogicOperator.Or:
                    Append(Dictionary.Or);
                    break;
            }
            return this;
        }

        public BlockBaseQueryBuilder WrapValue(object value)
        {
            if (value is Enum enumValue)
            {
                value = Convert.ChangeType(enumValue, enumValue.GetTypeCode());
            }
            if (value == null)
            {
                return Append(Dictionary.NullValue);
            }
            if (value is DateTime dtValue)
            {
                value = dtValue.ToString(CultureInfo.InvariantCulture);
            }
            return Append(value.IsNumber() ? value.ToString() : $"{Dictionary.LeftTextWrapper}{value}{Dictionary.RightTextWrapper}");
        }

        public BlockBaseQueryBuilder Table(BlockBaseColumn column)
        {
            Append(column.Table);
            return this;
        }

        public BlockBaseQueryBuilder Table(PropertyInfo column)
        {
            Append(column.ReflectedType.GetTableName());
            return this;
        }

        public BlockBaseQueryBuilder Column(BlockBaseColumn column)
        {
            Append(column.Table).TableColumnSeparator();
            Append(column.Name ?? Dictionary.AllSelector);
            return this;
        }

        public BlockBaseQueryBuilder Column(PropertyInfo column)
        {
            Append(column.ReflectedType.GetTableName()).TableColumnSeparator();
            Append(column.GetColumnName() ?? Dictionary.AllSelector);
            return this;
        }
        #endregion

        #endregion
    }
}
