namespace Compiler.Ast.Nodes;

internal class WhileNode : AstNode
{
    public PairNode Comparison { get; set; }
    public BlockNode Statements { get; } = new();
}