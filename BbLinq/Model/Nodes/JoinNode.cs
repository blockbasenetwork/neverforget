using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Model.Base;
using System.Reflection;

namespace BlockBase.BBLinq.Model.Nodes
{
    public class JoinNode : BinaryExpressionNode<BlockBaseComparisonOperator, PropertyNode, PropertyNode>
    {
        public JoinNode(PropertyNode left, PropertyNode right) : base(BlockBaseComparisonOperator.EqualTo, left, right) {}
        public JoinNode(PropertyInfo left, PropertyInfo right) : this(new PropertyNode(left), new PropertyNode(right)){}
    }
}
