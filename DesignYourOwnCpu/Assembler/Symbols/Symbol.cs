namespace Assembler.Symbols
{
    /// <summary>
    ///  a symbol in the symbol table, a name and an address
    /// </summary>
    public class Symbol
    {
        public Symbol(string name, ushort address)
        {
            Name = name;
            Address = address;
        } 
        
        public Symbol(string name)
        {
            Name = name;
        }

        public string Name { get;  }
        
        public ushort? Address { get; }
    }
}