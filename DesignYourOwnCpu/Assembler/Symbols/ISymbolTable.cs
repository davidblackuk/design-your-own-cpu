using System.Collections.Generic;

namespace Assembler.Symbols
{
    public interface ISymbolTable
    {
        /// <summary>
        ///     Define a symbol with it's address. the symbol may already have been referenced, but
        ///     if the address is already present this is a duplicate definition.
        /// </summary>
        /// <param name="name">symbol name</param>
        /// <param name="address">address for the symbol</param>
        void DefineSymbol(string name, ushort address);

        /// <summary>
        ///     A symbol has been referenced, that may not be yet defined. If it is not present in the symbol table,
        ///     add it. We create an entry on reference if not defined so at the end on the parse we can output a
        ///     list of undefined labels
        /// </summary>
        /// <param name="name">The symbol name</param>
        void ReferenceSymbol(string name);

        /// <summary>
        /// gets the list of all defined symbols
        /// </summary>
        IEnumerable<string> SymbolNames { get; }

        /// <summary>
        ///     Used during code generation, this retrieves the symbol information for code generation,
        ///     if a symbol is not peresnt at this point (forwards or backwards declaration), there is a problem
        ///     and an exception is thrown
        /// </summary>
        /// <param name="name">Name of the symbol to retrieve</param>
        /// <returns>The requested symbol</returns>
        Symbol GetSymbol(string name);

        /// <summary>
        ///     Saves the symbol table to the specified file
        /// </summary>
        /// <param name="symbolFile"></param>
        void Save(string symbolFile);
    }
}