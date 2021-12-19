namespace Compiler.Ast.Nodes
{
    internal class AssignmentNode : AstNode
    {
        public IdentifierNode Identifier { get; set; }
        public AstNode Expression = new ExpressionNode();
       
    }
}