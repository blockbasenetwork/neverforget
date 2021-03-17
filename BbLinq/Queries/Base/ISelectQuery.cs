using System;
using System.Linq.Expressions;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;

namespace BlockBase.BBLinq.Queries.Base
{
    public interface ISelectQuery : IQuery
    {
        #region Result Parsing
        public Type ReturnType { get; set; }
        public LambdaExpression Mapping { get; set; }

        #endregion

        #region PropertySelection
        public BlockBaseColumn[] SelectProperties { get; }
        #endregion

        #region Joins
        public JoinNode[] Joins { get; }
        #endregion

        #region Where
        public ExpressionNode Condition { get; }
        #endregion

        public int? Limit { get; }
        public int? Offset { get; }
    }
}
