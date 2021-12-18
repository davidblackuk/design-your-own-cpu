using System;
using System.Drawing;
using Compiler.Ast.Nodes;
using Pastel;

namespace Compiler.Ast
{
    internal class AbstractSyntaxTree : IAbstractSyntaxTree
    {
        public BlockNode Root { get; set; }
    }
}