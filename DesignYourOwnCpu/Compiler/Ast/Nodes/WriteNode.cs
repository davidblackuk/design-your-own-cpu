namespace Compiler.Ast.Nodes;

internal class WriteNode : AstNode
{
    public AstNode Expression { get; set; }
}