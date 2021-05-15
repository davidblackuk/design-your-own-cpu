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
            IRamFactory ramFactory = new RamFactory();
            
            RandomAccessMemory memory = ramFactory.Create(args.Length == 1 ? args[0] : null);
            
            Registers registers = new Registers();
            EmulatorInstructionFactory factory = new EmulatorInstructionFactory();
            IFlags flags = new Flags();
            CPU cpu = new CPU(memory, registers, flags, factory);
            cpu.Run();
        }
    }
}
