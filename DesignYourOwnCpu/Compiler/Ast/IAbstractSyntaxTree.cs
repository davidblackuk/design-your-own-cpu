namespace Compiler.Ast
{
    internal interface IAbstractSyntaxTree
    {
        BlockNode Root { get; set; }
        
        void Dump();
    }
}