using System.Collections.Generic;
using Assembler.Exceptions;

namespace Assembler.Symbols
{
    /// <summary>
    /// The symbol table for the assembler: for now we are not allowing local scoping for
    /// symbols defined in a file (eg loop symbols like %loop rather than .loop which is global)
    /// </summary>
    public class SymbolTable : ISymbolTable
    {
        private Dictionary<string, Symbol> table = new Dictionary<string, Symbol>();

        /// <summary>
        /// Define a symbol with it's address. the symbol may already have been referenced, but
        /// if the address is already present this is a duplicate definition  
        /// </summary>
        /// <param name="name">symbol name</param>
        /// <param name="address">address for the symbol</param>
        /// <exception cref="DuplicateSymbolException"></exception>
        public void DefineSymbol(string name, ushort address)
        {
            if (table.ContainsKey(name) && table[name].Address.HasValue)
            {
                throw new AssemblerException($"Duplicate symbol '{name}' defined");
            }
            else
            {
                table[name] = new Symbol(name, address);
            }
        }

        /// <summary>
        /// A symbol has been referenced, that may not be yet defined. If it is not present in the symbol table, add it
        /// </summary>
        /// <param name="name">The symbol name</param>
        public void ReferenceSymbol(string name)
        {
            if (!table.ContainsKey(name))
            {
                table[name] = new Symbol( name );
            }
        }

        /// <summary>
        /// Used during code generation, this retrieves the symbol information for code generation,
        /// if a symbol is not peresnt at this point (forwards or backwards declaration), there is a problem
        /// and an exception is thrown
        /// </summary>
        /// <param name="name">Name of the symbol to retrieve</param>
        /// <returns>The requested symbol</returns>
        /// <exception cref="???"></exception>
        public Symbol GetSymbol(string name)
        {
            if (!table.ContainsKey(name))
                throw new AssemblerException($"Undefined symbol: {name}");
            return table[name];
        }
    }
}