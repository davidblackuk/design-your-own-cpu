using System;
using System.Drawing;
using Assembler.Instructions;
using Pastel;

namespace Assembler.Extensions;

public static class AssemblerInstructionExtensions
{
    public static void ToConsole(this IAssemblerInstruction instruction, ushort address)
    {
        Console.Write($"0x{address:X4}  ".Pastel(Color.Goldenrod));
        Console.Write($"{instruction.BytesString()} ".Pastel(Color.Orchid));
        Console.WriteLine($" # {instruction}".Pastel(Color.Teal));
    }
}