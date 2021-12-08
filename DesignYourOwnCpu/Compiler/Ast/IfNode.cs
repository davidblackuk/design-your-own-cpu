namespace Compiler.Ast
{
    internal class IfNode : AstNode
    {
        public PairNode Comparison { get; set; }
        public BlockNode Statements { get; } = new();

    }
}