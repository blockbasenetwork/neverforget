using BlockBase.BBLinq.Model.Base;

namespace BlockBase.BBLinq.Model.Nodes
{
    public class ValueNode : ExpressionNode
    {
        public object Value { get; }

        public ValueNode(object value)
        {
            Value = value;
        }
    }
}
