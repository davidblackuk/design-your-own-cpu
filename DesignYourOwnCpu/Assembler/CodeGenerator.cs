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
        private readonly IAssemblerConfig config;

        public CodeGenerator(ISymbolTable symbolTable, IRandomAccessMemory ram, IAssemblerConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
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

                if (!config.QuietOutput)
                {
                    instruction.ToConsole(address);
                }

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