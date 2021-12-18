namespace Compiler.Ast.Nodes
{
    internal class IfNode : AstNode
    {
        public PairNode Comparison { get; set; }
        public BlockNode Statements { get; } = new();

    }
}