using System;
using System.Diagnostics.CodeAnalysis;

namespace Emulator.Instructions.Interrupts
{
    /// <summary>
    ///     Reads a word from the console.
    /// </summary>
    [ExcludeFromCodeCoverage] // this would be an integration test
    public class WriteWordInterrupt : IInterrupt
    {
        public void Execute(ICPU cpu)
        {
            Console.Write($"0x{cpu.Registers[0]:X4}");
        }
    }
}