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
        private readonly RandomAccessMemory ram;

        public CodeGenerator(ISymbolTable symbolTable, RandomAccessMemory ram)
        {
            this.symbolTable = symbolTable ?? throw new ArgumentNullException(nameof(symbolTable));
            this.ram = ram ?? throw new ArgumentNullException(nameof(ram));
        }

        public void GenerateCode(IEnumerable<IInstruction> instructions)
        {
            
            ushort address = 0;
            foreach (var instruction in instructions)
            {
                if (instruction.RequresSymbolResolution)
                {
                    var symbol = symbolTable.GetSymbol(instruction.Symbol);
                    instruction.StoreData(symbol.Address.Value);
                }
                
                Console.Write($"0x{address:X4}  ".Pastel(Color.Goldenrod) );
                Console.Write($"{instruction.BytesString()} ".Pastel(Color.Orchid) );
                
                
                // patch up symbols if required
                ram[address++] = instruction.OpCode;
                ram[address++] = instruction.Register;
                ram[address++] = instruction.ByteHigh;
                ram[address++] = instruction.ByteLow;
                
                Console.WriteLine($" # {instruction}".Pastel(Color.Teal));
            }
        }
    }
}