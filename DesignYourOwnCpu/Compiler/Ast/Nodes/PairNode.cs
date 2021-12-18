namespace Compiler.Ast.Nodes
{
    internal class PairNode : AstNode
    {
        public string Operator { get; set; }

        public AstNode Left { get; set; }
        public AstNode Right { get; set; }

        public override string ToString()
        {
            return $"{Operator}";
        }
    }
}