namespace Compiler.Ast
{
    internal interface ISymbolTable
    {
        /// <summary>
        /// Declare an identifier
        /// </summary>
        /// <param name="identifier">The identifier to declare</param>
        void Declare(string identifier);

        /// <summary>
        /// Check if an an entry for the specified identifier exists in the symbol table.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists(string identifier);
    }
}