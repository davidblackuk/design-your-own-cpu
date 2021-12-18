namespace Compiler.Ast.Nodes
{
    internal class AssignmentNode : AstNode
    {
        public string Identifier { get; set; }
        public AstNode Expression = new ExpressionNode();
       
    }
}