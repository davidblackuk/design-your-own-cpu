﻿using System;
using System.Drawing;
using Assembler.Symbols;
using Pastel;

namespace Assembler.Extensions
{
    public static class SymbolExtensions
    {
        public static void ToConsole(this Symbol symbol)
        {
            Console.Write($"0x{symbol.Address:X4} ".Pastel(Color.Goldenrod));
            Console.WriteLine($"{symbol.Name}".Pastel(Color.Orchid));
        }
    }
}