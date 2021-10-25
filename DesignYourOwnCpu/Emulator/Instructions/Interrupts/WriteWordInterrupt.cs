using System;

namespace Emulator.Instructions.Interrupts
{
    /// <summary>
    ///     Reads a word from the console.
    /// </summary>
    public class WriteWordInterrupt : IInterrupt
    {
        public void Execute(ICPU cpu)
        {
            Console.Write($"0x{cpu.Registers[0]:X4}");
        }
    }
}