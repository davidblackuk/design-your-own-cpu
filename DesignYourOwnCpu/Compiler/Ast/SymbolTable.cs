using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Extensions.Logging;
using Pastel;

namespace Compiler.Ast
{
    internal class SymbolTable: Dictionary<string, Symbol>, ISymbolTable
    {
        private readonly ILogger<SymbolTable> logger;

        public SymbolTable(ILogger<SymbolTable> logger)
        {
            this.logger = logger;
        }
        
        public void Declare(string identifier)
        {
            if (ContainsKey(identifier))
            {
                logger.LogWarning($"Duplicate declaration of symbol: {identifier}".Pastel(Color.Tomato));
                return;
            }
            Add(identifier, new Symbol(identifier));
            logger.LogDebug($"Declare identifier: {identifier}");
        }
    }
}