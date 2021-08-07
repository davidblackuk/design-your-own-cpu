using Assembler.LineSources;
using Assembler.Symbols;
using Shared;

namespace Assembler
{
    public interface IAssembler
    {
        public IRandomAccessMemory Ram { get; }
        
        public ISymbolTable SymbolTable { get; }
        void Assemble(ILineSource lineSource);
    }
}