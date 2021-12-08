namespace Compiler.Ast
{
    internal class IdentifierNode : AstNode
    {
        public string Value { get; }

        public IdentifierNode(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}