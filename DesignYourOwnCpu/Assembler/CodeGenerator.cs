using System;
using System.Collections.Generic;
using Assembler.Extensions;
using Assembler.Instructions;
using Assembler.Symbols;
using Shared;

namespace Assembler
{
    public class CodeGenerator : ICodeGenerator
    {
        public CodeGenerator(ISymbolTable symbolTable, IRandomAccessMemory ram)
        {
            SymbolTable = symbolTable ?? throw new ArgumentNullException(nameof(symbolTable));
            Ram = ram ?? throw new ArgumentNullException(nameof(ram));
        }

        public IRandomAccessMemory Ram { get; }

        public ISymbolTable SymbolTable { get; }

        public void GenerateCode(IEnumerable<IAssemblerInstruction> instructions)
        {
            ushort address = 0;
            foreach (var instruction in instructions)
            {
                ResolveSymbolsIfRequired(instruction);
                instruction.WriteBytes(Ram, address);
                instruction.ToConsole(address);
                address += instruction.Size;
            }
        }

        private void ResolveSymbolsIfRequired(IAssemblerInstruction instruction)
        {
            if (instruction.RequresSymbolResolution)
            {
                var symbol = SymbolTable.GetSymbol(instruction.Symbol);
                instruction.StoreData(symbol.Address.Value);
            }
        }
    }
}