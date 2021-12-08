namespace Compiler.Ast
{
    internal class WhileNode : AstNode
    {
        public PairNode Comparison { get; set; }
        public BlockNode Statements { get; } = new();
    }
}