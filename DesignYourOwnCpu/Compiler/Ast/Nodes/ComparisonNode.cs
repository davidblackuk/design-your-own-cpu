namespace Compiler.Ast.Nodes;

internal class ComparisonNode : AstNode
{
    public string Operation { get; set;  }
    public ExpressionNode Left { get; set; }
    public ExpressionNode Right { get; set; }
}