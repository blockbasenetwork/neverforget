namespace BlockBase.BBLinq.Model.Base
{
    public class BinaryExpressionNode<TOperator, TLeft, TRight> :ExpressionNode
    {
        public TOperator Operator { get; }
        public TLeft Left { get;  }
        public TRight Right { get; }

        public BinaryExpressionNode(TOperator @operator, TLeft left, TRight right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }
    }
}
