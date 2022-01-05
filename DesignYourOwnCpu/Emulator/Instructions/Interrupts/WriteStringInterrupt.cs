using System;
using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Emulator.Instructions.Interrupts
{
    /// <summary>
    ///     Writes out a zero terminated MASCII string located at the address held in register 0
    ///     to the console in .net land.
    /// </summary>
    [ExcludeFromCodeCoverage] // this would be an integration test
    public class WriteStringInterrupt : IInterrupt
    {
        public void Execute(ICpu cpu)
        {
            var address = cpu.Registers[0];
            while (cpu.Memory[address] != 0)
            {
                var mascii = cpu.Memory[address];
                if (mascii == MockAsciiMapper.NewLine)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(MockAsciiMapper.ConvertByteToChar(mascii));
                }

                address++;
            }
        }
    }
}