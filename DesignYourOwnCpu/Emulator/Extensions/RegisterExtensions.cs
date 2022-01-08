using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Pastel;

namespace Emulator.Extensions;

[ExcludeFromCodeCoverage]
public static class RegisterExtensions
{
    public static void ToConsole(this IRegisters registers)
    {
        WriteRegister("PC", registers.ProgramCounter);
        WriteRegister("SP", registers.StackPointer);
        Console.WriteLine();

        for (byte index = 0; index < 8; index++)
        {
            WriteRegister($"R{index}", registers[index]);
        }

        Console.WriteLine();
    }

    private static void WriteRegister(string name, ushort value)
    {
        Console.Write($"{name}: ".Pastel(Color.Goldenrod));
        Console.Write($"0X{value:X4}: ".Pastel(Color.Orchid));
    }
}