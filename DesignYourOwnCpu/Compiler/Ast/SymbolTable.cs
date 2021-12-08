using System;
using System.Collections.Generic;
using System.Drawing;
using Pastel;

namespace Compiler.Ast
{
    internal class SymbolTable: Dictionary<string, Symbol>, ISymbolTable
    {
        public void Declare(string identifier)
        {
            if (ContainsKey(identifier))
            {
                Console.WriteLine($"Duplicate declaration of symbol: {identifier}".Pastel(Color.Tomato));
                return;
            }
            Add(identifier, new Symbol(identifier));
            Console.WriteLine($"Declare identifier: {identifier}");
        }
    }
}