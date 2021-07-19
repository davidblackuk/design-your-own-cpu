using System;
using Shared;

namespace Emulator.Instructions.Interrupts
{
    /// <summary>
    ///     Writes out a zero terminated MASCII string located at the address held in register 0
    ///     to the console in .net land.
    /// </summary>
    public class WriteStringInterrupt : IInterrupt
    {
        public void Execute(ICPU cpu)
        {
            var address = cpu.Registers[0];
            while (cpu.Memory[address] != 0)
            {
                var mascii = cpu.Memory[address];
                if (mascii == MockAsciiMapper.NewLine)
                    Console.WriteLine();
                else
                    Console.Write(MockAsciiMapper.ConvertByteToChar(mascii));
                address++;
            }
        }
    }
}