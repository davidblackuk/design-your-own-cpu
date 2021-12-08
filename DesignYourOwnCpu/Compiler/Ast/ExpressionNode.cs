namespace Compiler.Ast
{
    internal class ExpressionNode : AstNode
    {
        // operator
        private ExpressionNode Left { get; set; }
        private ExpressionNode Right { get; set; }
    }
}