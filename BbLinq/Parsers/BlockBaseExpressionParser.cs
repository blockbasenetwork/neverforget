using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BlockBase.BBLinq.Enumerables;
using BlockBase.BBLinq.Exceptions;
using BlockBase.BBLinq.ExtensionMethods;
using BlockBase.BBLinq.Model.Base;
using BlockBase.BBLinq.Model.Database;
using BlockBase.BBLinq.Model.Nodes;
using BlockBase.BBLinq.Validators;

namespace BlockBase.BBLinq.Parsers
{
    public static class BlockBaseExpressionParser
    {
        private static int _depth;
        private static IReadOnlyCollection<ParameterExpression> _parameters;

        #region ParseCondition

        public static ExpressionNode Parse(LambdaExpression expression)
        {
            if (expression == null)
            {
                return null;
            }
            _depth = 0;
            _parameters = expression.Parameters;
            var result = Parse(expression.Body);
            return result;
        }

        private static ExpressionNode Parse(Expression expression)
        {
            switch (expression)
            {
                case BinaryExpression binaryExpression:
                    return ParseBinaryExpression(binaryExpression);
                case MemberExpression memberExpression:
                    return ParseMemberExpression(memberExpression);
                case ConstantExpression constantExpression:
                    return ParseConstantExpression(constantExpression);
                case UnaryExpression unaryExpression:
                    return ParseUnaryExpression(unaryExpression);
                case MethodCallExpression methodCallExpression:
                    return ParseMethodCallExpression(methodCallExpression);
            }
            throw new UnsupportedExpressionException(expression);
        }

        #region Method Call

        private static ExpressionNode ParseMethodCallExpression(MethodCallExpression expression)
        {
            var method = expression.Method;
            var valueExpression = expression.Object;
            object value = null;
            if (valueExpression is ConstantExpression constantValue)
            {
                value = constantValue.Value;
            }
            else if (valueExpression is MemberExpression valueMember)
            {
                value = valueMember.GetValue();
            }
            if (value != null)
            {
                var result = method.Invoke(value, new object[] { });
                return new ValueNode(result);
            }
            throw new UnsupportedExpressionException(expression);
        }

        #endregion

        #region Unary
        private static ExpressionNode ParseUnaryExpression(UnaryExpression expression)
        {
            if (expression.IsNot())
            {
                return ParseNot(expression);
            }
            if (expression.IsNegate())
            {
                return null;
            }
            if (expression.IsUnaryPlus())
            {
                return null;
            }
            throw new UnsupportedExpressionException(expression);
        }

        #endregion

        #region Sub-Unary
        private static ExpressionNode ParseNot(UnaryExpression expression)
        {
            var operandNode = Parse(expression.Operand);
            if (operandNode is PropertyNode operandProperty)
            {
                return GetIsNotNode(operandProperty);
            }

            if (operandNode is ComparisonNode { Left: PropertyNode left })
            {
                return GetIsNotNode(left);
            }

            if (operandNode is ValueNode { Value: bool value })
            {
                return new ValueNode(!value);
            }
            throw new UnsupportedExpressionException(expression);
        }
        #endregion

        #region Binary

        private static ExpressionNode ParseBinaryExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedComparison())
            {
                return ParseComparisonExpression(expression);
            }

            if (expression.IsAcceptedArithmetic())
            {
                return ParseArithmeticExpression(expression);
            }

            if (expression.IsAcceptedLogic())
            {
                return ParseLogicExpression(expression);
            }
            throw new UnsupportedExpressionException(expression);
        }

        #endregion

        #region Sub-Binary
        private static ExpressionNode ParseComparisonExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedComparison())
            {
                var @operator = ParseComparisonOperator(expression.NodeType);
                var (left, right) = ParseSides(expression);
                ComparisonNode result = null;
                switch (left)
                {
                    case ValueNode value when right is PropertyNode property:
                        result = new ComparisonNode(@operator, property, value);
                        break;
                    case PropertyNode leftProperty:
                        switch (right)
                        {
                            case PropertyNode rightProperty:
                                result = new ComparisonNode(@operator, leftProperty, rightProperty);
                                break;
                            case ValueNode rightValue:
                                result = new ComparisonNode(@operator, leftProperty, rightValue);
                                break;
                        }
                        break;
                }
                ExpressionNodeValidator.ValidateComparisonNode(result);
                return result;
            }
            throw new UnsupportedExpressionException(expression);
        }

        private static ExpressionNode ParseLogicExpression(BinaryExpression expression)
        {
            if (expression.IsAcceptedLogic())
            {
                var @operator = ParseLogicOperator(expression.NodeType);
                var (left, right) = ParseSides(expression);
                if (left is PropertyNode propLeft)
                {
                    left = GetIsNode(propLeft);
                }
                if (right is PropertyNode propRight)
                {
                    right = GetIsNode(propRight);
                }
                switch (left)
                {
                    case ComparisonNode comparisonLeft when right is ComparisonNode comparisonRight:
                        return new LogicNode(@operator, comparisonLeft, comparisonRight);
                    case ComparisonNode comparisonLeft when right is LogicNode logicRight:
                        return new LogicNode(@operator, comparisonLeft, logicRight);
                    case LogicNode logicLeft when right is ComparisonNode comparisonRight:
                        return new LogicNode(@operator, logicLeft, comparisonRight);
                    case LogicNode logicLeft when right is LogicNode logicRight:
                        return new LogicNode(@operator, logicLeft, logicRight);
                }
            }
            throw new UnsupportedExpressionException(expression);
        }

        private static ExpressionNode ParseArithmeticExpression(BinaryExpression expression)
        {
            var @operator = ParseArithmeticOperator(expression.NodeType);
            var (left, right) = ParseSides(expression);
            switch (left)
            {
                case ValueNode leftValue when right is ValueNode rightValue:
                    if (leftValue.Value.IsNumber() && leftValue.Value.IsNumber())
                    {
                        var result = ExecuteOperatorOnNumbers(@operator, leftValue.Value, rightValue.Value);
                        if (result == null)
                        {
                            throw new UnsupportedExpressionException(expression);
                        }
                        return new ValueNode(result);
                    }
                    if (@operator == BlockBaseArithmeticOperator.Add)
                    {
                        return new ValueNode(leftValue.Value.ToString() + rightValue.Value);
                    }
                    break;
            }
            throw new UnsupportedExpressionException(expression);
        }



        #endregion

        #region Sub-Binary

        public static dynamic ExecuteOperatorOnNumbers(BlockBaseArithmeticOperator @operator, dynamic left, dynamic right)
        {
            switch (@operator)
            {
                case BlockBaseArithmeticOperator.Add:
                    return left + right;
                case BlockBaseArithmeticOperator.Subtract:
                    return left - right;
                case BlockBaseArithmeticOperator.Divide:
                    return left / right;
                case BlockBaseArithmeticOperator.Multiply:
                    return left * right;
                case BlockBaseArithmeticOperator.Modulo:
                    return left % right;
            }
            return null;
        }

        public static (ExpressionNode, ExpressionNode) ParseSides(BinaryExpression expression)
        {
            _depth++;
            var left = Parse(expression.Left);
            var right = Parse(expression.Right);
            _depth--;
            return (left, right);
        }

        #endregion

        #region Member Access 
        private static ExpressionNode ParseMemberExpression(MemberExpression expression)
        {
            if (expression.IsPropertyAccess(_parameters))
            {
                var node = new PropertyNode(expression.Member as PropertyInfo);
                if (_depth == 0)
                {
                    var isNode = GetIsNode(node);
                    if (isNode != null)
                    {
                        return isNode;
                    }
                }
                return node;
            }
            var value = expression.GetValue();
            if (value != null && _depth > 0)
            {
                return new ValueNode(value);
            }
            throw new UnsupportedExpressionException(expression);
        }
        #endregion

        #region Constant
        private static ExpressionNode ParseConstantExpression(ConstantExpression expression)
        {
            if (_depth == 0)
            {
                throw new UnsupportedExpressionException(expression);
            }
            return new ValueNode(expression.Value);
        }

        #endregion

        #region Auxiliary
        private static ComparisonNode GetIsNode(PropertyNode node, bool value = true)
        {
            return node.Property.PropertyType != typeof(bool) ? null :
                new ComparisonNode(BlockBaseComparisonOperator.EqualTo, node, new ValueNode(value));
        }

        private static ComparisonNode GetIsNotNode(PropertyNode node)
        {
            return GetIsNode(node, false);
        }


        #endregion

        #region Operators
        private static BlockBaseComparisonOperator ParseComparisonOperator(ExpressionType expressionOperator)
        {
            switch (expressionOperator)
            {
                case ExpressionType.Equal:
                    return BlockBaseComparisonOperator.EqualTo;
                case ExpressionType.NotEqual:
                    return BlockBaseComparisonOperator.DifferentFrom;
                case ExpressionType.GreaterThan:
                    return BlockBaseComparisonOperator.GreaterThan;
                case ExpressionType.GreaterThanOrEqual:
                    return BlockBaseComparisonOperator.GreaterOrEqualTo;
                case ExpressionType.LessThan:
                    return BlockBaseComparisonOperator.LessThan;
                case ExpressionType.LessThanOrEqual:
                    return BlockBaseComparisonOperator.LessOrEqualTo;
                default:
                    throw new UnsupportedExpressionOperatorException(expressionOperator);
            }
        }

        private static BlockBaseArithmeticOperator ParseArithmeticOperator(ExpressionType expressionOperator)
        {
            switch (expressionOperator)
            {
                case ExpressionType.Add:
                    return BlockBaseArithmeticOperator.Add;
                case ExpressionType.Subtract:
                    return BlockBaseArithmeticOperator.Subtract;
                case ExpressionType.Divide:
                    return BlockBaseArithmeticOperator.Divide;
                case ExpressionType.Multiply:
                    return BlockBaseArithmeticOperator.Multiply;
                case ExpressionType.Modulo:
                    return BlockBaseArithmeticOperator.Modulo;
                default:
                    throw new UnsupportedExpressionOperatorException(expressionOperator);
            }
        }

        private static BlockBaseLogicOperator ParseLogicOperator(ExpressionType expressionOperator)
        {
            switch (expressionOperator)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return BlockBaseLogicOperator.And;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return BlockBaseLogicOperator.Or;
                default:
                    throw new UnsupportedExpressionOperatorException(expressionOperator);
            }
        }
        #endregion

        #endregion

        #region Parse Selection

        public static BlockBaseColumn[] ParseSelectionColumns(LambdaExpression expression)
        {
            _parameters = expression.Parameters;
            BlockBaseColumn[] result;
            switch (expression.Body)
            {
                case MemberExpression memberExpression:
                    result = ParseMemberSelection(memberExpression);
                    break;
                case MemberInitExpression memberInitExpression:
                    result = ParseMemberInitSelection(memberInitExpression);
                    break;
                case NewExpression newExpression:
                    result = ParseNewSelection(newExpression);
                    break;
                default:
                    result = null;
                    break;
            }

            if (result != null)
            {
                var selectionProperties = new List<BlockBaseColumn>();
                foreach (var property in result)
                {
                    var exists = false;
                    foreach (var existingProperty in selectionProperties)
                    {
                        if (existingProperty.Name != property.Name || existingProperty.Table != property.Table)
                        {
                            continue;
                        }
                        exists = true;
                        break;
                    }

                    if (!exists)
                    {
                        selectionProperties.Add(property);
                    }
                }
                return selectionProperties.ToArray();
            }
            return null;
        }

        public static BlockBaseColumn[] ParseMemberSelection(MemberExpression memberExpression)
        {
            return new []{BlockBaseColumn.From(memberExpression.Member as PropertyInfo)};
        }

        public static BlockBaseColumn[] ParseMemberInitSelection(MemberInitExpression memberInitExpression)
        {
            var columns = new List<BlockBaseColumn>();
            var bindings = memberInitExpression.Bindings;
            foreach (var binding in bindings)
            {
                if (binding is MemberAssignment assignment)
                {
                    if (assignment.Expression is MemberExpression memberExpression)
                    {
                        columns.Add(BlockBaseColumn.From(memberExpression.Member as PropertyInfo));
                    }
                    else
                    {
                        var properties = RetrievePropertyFromExpression(assignment.Expression);
                        foreach (var property in properties)
                        {
                            columns.Add(BlockBaseColumn.From(property));
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            return columns.ToArray();
        }

        public static BlockBaseColumn[] ParseNewSelection(NewExpression newExpression)
        {
            var columns = new List<BlockBaseColumn>();
            var arguments = newExpression.Arguments;
            foreach (var argument in arguments)
            {
                if (argument is MemberExpression memberExpression)
                {
                    columns.Add(BlockBaseColumn.From(memberExpression.Member as PropertyInfo));
                }
                else
                {
                    var properties = RetrievePropertyFromExpression(argument);
                    foreach (var property in properties)
                    {
                        columns.Add(BlockBaseColumn.From(property));
                    }
                }
            }
            return columns.ToArray();
        }

        private static PropertyInfo[] RetrievePropertyFromExpression(Expression expression)
        {
            var properties = new List<PropertyInfo>();
            switch (expression)
            {
                case MemberExpression memberExpression:
                    var member = memberExpression.Member;
                    if (member is PropertyInfo property)
                    {
                        foreach (var parameter in _parameters)
                        {
                            var type = parameter.Type;
                            if (type == property.ReflectedType)
                            {
                                return new[] {property};
                            }
                        }
                    }
                    return null;
                case BinaryExpression binaryExpression:
                    var left = RetrievePropertyFromExpression(binaryExpression.Left);
                    var right = RetrievePropertyFromExpression(binaryExpression.Right);
                    if (left != null)
                    {
                        properties.AddRange(left);
                    }

                    if (right != null && right.Length != 0)
                    {
                        properties.AddRange(right);
                    }

                    return properties.ToArray();
                case MethodCallExpression callExpression:
                    var obj = RetrievePropertyFromExpression(callExpression.Object);
                    if (obj != null)
                    {
                        properties.AddRange(obj);
                    }
                    return properties.ToArray();
            }
            return properties.ToArray();
        }
        #endregion
    }
}
