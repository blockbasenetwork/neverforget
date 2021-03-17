using BlockBase.BBLinq.Model.Base;
using System.Reflection;

namespace BlockBase.BBLinq.Model.Nodes
{
    public class PropertyNode : ExpressionNode
    {
        public PropertyInfo Property { get; }

        public PropertyNode(PropertyInfo property)
        {
            Property = property;
        }
    }
}
