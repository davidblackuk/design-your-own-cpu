namespace Compiler.Ast
{
    internal class ComparisonNode : AstNode
    {
        public string Operation { get; set;  }
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }
}