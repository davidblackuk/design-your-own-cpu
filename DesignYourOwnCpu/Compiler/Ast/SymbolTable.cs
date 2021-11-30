using System;
using System.Collections.Generic;
using System.Drawing;
using Pastel;

namespace Compiler.Ast
{
    internal interface ISymbolTable
    {
        void Declare(string identifier);
    }

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
    
    internal class Symbol
    {
        public Symbol(string name)
        {
            Name = name;
        }
        
        public string Name { get; private set; }
    }

    
}