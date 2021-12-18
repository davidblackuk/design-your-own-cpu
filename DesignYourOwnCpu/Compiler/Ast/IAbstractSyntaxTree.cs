using Compiler.Ast.Nodes;

namespace Compiler.Ast
{
    internal interface IAbstractSyntaxTree
    {
        BlockNode Root { get; set; }
    }
}