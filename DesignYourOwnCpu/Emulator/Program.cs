using System;
using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Shared;

namespace Emulator
{
    [ExcludeFromCodeCoverageAttribute]
    class Program
    {
        static void Main(string[] args)
        {
            RandomAccessMemory memory = new RandomAccessMemory();

            memory[0] = 0xFF; // NOP
            memory[4] = 0xFF; // NOP
            memory[8] = 0xFE; // HALT
            
            Registers registers = new Registers();
            EmulatorInstructionFactory factory = new EmulatorInstructionFactory();
            IFlags flags = new Flags();
            CPU cpu = new CPU(memory, registers, flags, factory);
            cpu.Run();
        }
    }
}
