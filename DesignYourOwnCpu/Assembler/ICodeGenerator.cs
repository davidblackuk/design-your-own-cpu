using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.Symbols;
using Shared;

namespace Assembler;

public interface ICodeGenerator
{
    public IRandomAccessMemory Ram { get; }

    public ISymbolTable SymbolTable { get; }

    void GenerateCode(IEnumerable<IAssemblerInstruction> instructions);
}