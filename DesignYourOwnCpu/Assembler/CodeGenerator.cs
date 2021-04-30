﻿using System;
using System.Collections.Generic;
using Assembler.Instructions;
using Assembler.Symbols;
using Shared;

namespace Assembler
{
    public class CodeGenerator
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
                // patch up symbols if required
                ram[address++] = instruction.OpCode;
                ram[address++] = instruction.Register;
                ram[address++] = instruction.ByteHigh;
                ram[address++] = instruction.ByteLow;
                
                Console.WriteLine(instruction);
            }
        }
    }
}