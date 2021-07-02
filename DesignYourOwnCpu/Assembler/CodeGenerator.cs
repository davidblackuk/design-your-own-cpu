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
        private readonly IRandomAccessMemory ram;
        private readonly ISymbolTable symbolTable;

        public CodeGenerator(ISymbolTable symbolTable, IRandomAccessMemory ram)
        {
            this.symbolTable = symbolTable ?? throw new ArgumentNullException(nameof(symbolTable));
            this.ram = ram ?? throw new ArgumentNullException(nameof(ram));
        }

        public void GenerateCode(IEnumerable<IAssemblerInstruction> instructions)
        {
            ushort address = 0;
            foreach (var instruction in instructions)
            {
                ResolveSymbolsIfRequired(instruction);
                instruction.WriteBytes(ram, address);
                instruction.ToConsole(address);
                address += instruction.Size;
            }
        }

        private void ResolveSymbolsIfRequired(IAssemblerInstruction instruction)
        {
            if (instruction.RequresSymbolResolution)
            {
                var symbol = symbolTable.GetSymbol(instruction.Symbol);
                instruction.StoreData(symbol.Address.Value);
            }
        }
    }
}