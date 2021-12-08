using System.Collections.Generic;

namespace Compiler.Ast
{
    internal class BlockNode : AstNode
    {
        public List<AstNode> Nodes = new List<AstNode>();

        public T Add<T>(T node) where T : AstNode
        {
            Nodes.Add(node);
            return node;
        }
    }
}