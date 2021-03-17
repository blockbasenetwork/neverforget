using System;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.Model.Nodes;

namespace BlockBase.BBLinq.Validators
{
    public static class ExpressionNodeValidator
    {
        public static void ValidateComparisonNode(ComparisonNode node)
        {
            if (node.Left is PropertyNode leftProperty)
            {
                if (node.Right is ValueNode rightValue)
                {
                    if (leftProperty.Property.PropertyType == rightValue.Value.GetType())
                    {
                        return;
                    }
                }
                else if (node.Right is PropertyNode rightProperty)
                {
                    if (leftProperty.Property.PropertyType == rightProperty.Property.PropertyType)
                    {
                        return;
                    }
                }
            }
            throw new InvalidComparisonExpressionException(node.Operator, node.Left, node.Right);
        }

        public static void ValidateLogicNode(LogicNode node)
        {
            switch (node.Left)
            {
                case ComparisonNode comparisonLeft:
                    ValidateComparisonNode(comparisonLeft);
                    return;
                case LogicNode logicLeft:
                    ValidateLogicNode(logicLeft);
                    return;
            }
            switch (node.Right)
            {
                case ComparisonNode comparisonLeft:
                    ValidateComparisonNode(comparisonLeft);
                    return;
                case LogicNode logicLeft:
                    ValidateLogicNode(logicLeft);
                    return;
            }

            throw new InvalidLogicExpressionException(node);
        }
    }
}
