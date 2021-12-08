namespace Compiler.Ast
{
    internal interface ISymbolTable
    {
        void Declare(string identifier);
    }
}