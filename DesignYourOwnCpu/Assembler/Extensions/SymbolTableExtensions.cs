using System.Diagnostics.CodeAnalysis;
using Assembler.Symbols;

namespace Assembler.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SymbolTableExtensions
    {
        public static void ToConsole(this ISymbolTable table)
        {
           
            foreach (var symbol in table.SymbolNames)
            {
                var definition = table.GetSymbol(symbol);
                definition.ToConsole();
            }
        }
    }
}