using System;
using System.Linq.Expressions;
using BlockBase.BBLinq.Builders;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Queries.Base;

namespace BlockBase.BBLinq.Queries.BlockBaseQueries
{
    public class BlockBaseRecordSelectQuery : BlockBaseQuery, ISelectQuery
    {
        public BlockBaseRecordSelectQuery(Type returnType, LambdaExpression mapping, BlockBaseColumn[] selectedProperties, JoinNode[] joins, ExpressionNode condition, int? limit, int? offset,  bool isEncrypted) : base(isEncrypted)
        {
            ReturnType = returnType;
            Mapping = mapping;
            SelectProperties = selectedProperties;
            Joins = joins;
            Condition = condition;
            Limit = limit;
            Offset = offset;
        }

        public Type ReturnType { get; set; }
        public LambdaExpression Mapping { get; set; }
        public BlockBaseColumn[] SelectProperties { get; }
        public JoinNode[] Joins { get; }
        public ExpressionNode Condition { get; }
        public int? Limit { get; }
        public int? Offset { get; }

        public override string GenerateQueryString()
        {
            var queryBuilder = new BlockBaseQueryBuilder();
            queryBuilder.SelectRecord(SelectProperties, Joins, Condition, Limit, Offset, IsEncrypted);
            return queryBuilder.ToString();
        }
    }
}
