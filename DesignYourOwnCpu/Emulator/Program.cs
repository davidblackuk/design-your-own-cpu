using System;
using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Emulator
{
    [ExcludeFromCodeCoverageAttribute]
    class Program
    {
        static void Main(string[] args)
        {
            RandomAccessMemory memory = new RandomAccessMemory();
            Registers registers = new Registers();
            CPU cpu = new CPU(memory, registers);
        }
    }
}
