using System;

namespace Compiler.Ast
{
    internal interface IAbstractSyntaxTree
    {
        AstNode Root { get; }

        void Dump();
    }

    internal class AbstractSyntaxTree : IAbstractSyntaxTree
    {
        public AstNode Root { get; } = new AstNode();

        public void Dump()
        {
            Dump(Root, 0);
        }

        private void Dump(AstNode node, int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("    ");
            }
            
            Console.WriteLine(node.GetType().Name);

            foreach (AstNode child  in node.Nodes)
            {
                Dump(child, level + 1);
            }
            
        }
    }
}