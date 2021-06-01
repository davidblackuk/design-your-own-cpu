using System;
using System.Drawing;
using Assembler.Symbols;
using Pastel;

namespace Assembler.Extensions
{
    public static class SymbolExtensions
    {
        public static void ToConsole(this Symbol symbol)
        {
            Console.Write($"{symbol.Name}: ".Pastel(Color.Goldenrod));
            Console.WriteLine($"0x{symbol.Address:X4}".Pastel(Color.Orchid));
        }
    }
}