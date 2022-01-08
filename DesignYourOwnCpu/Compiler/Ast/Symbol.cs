namespace Compiler.Ast;

internal class Symbol
{
    public Symbol(string name)
    {
        Name = name;
    }
        
    public string Name { get; private set; }
}