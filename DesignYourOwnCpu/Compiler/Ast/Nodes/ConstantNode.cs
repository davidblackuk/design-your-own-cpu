namespace Compiler.Ast.Nodes
{
    internal class ConstantNode : AstNode
    {
        public ushort Value { get; }

        public ConstantNode(ushort value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}