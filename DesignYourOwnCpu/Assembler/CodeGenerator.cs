using System;
using System.Collections.Generic;
using System.Drawing;
using Assembler.Instructions;
using Assembler.Symbols;
using Pastel;
using Shared;

namespace Assembler
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly ISymbolTable symbolTable;
        private readonly IRandomAccessMemory ram;

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
                
                Console.Write($"0x{address:X4}  ".Pastel(Color.Goldenrod) );
                Console.Write($"{instruction.BytesString()} ".Pastel(Color.Orchid) );
                
                StoreBytesForInstruction(address, instruction);
                address += instruction.Size;
                
                Console.WriteLine($" # {instruction}".Pastel(Color.Teal));
            }
        }

        private void StoreBytesForInstruction(ushort address, IAssemblerInstruction instruction)
        {
            ram[address++] = instruction.OpCode;
            ram[address++] = instruction.Register;
            ram[address++] = instruction.ByteHigh;
            ram[address++] = instruction.ByteLow;
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