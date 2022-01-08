using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Assembler.Exceptions;
using Pastel;

namespace Assembler.Extensions;

[ExcludeFromCodeCoverage]
public static class AssemblerExceptionExtensions
{
    public static void ToConsole(this AssemblerException exception)
    {
        Console.Write("[ERROR] ".Pastel(Color.Tomato));
        Console.WriteLine(exception.Message.Pastel(Color.Goldenrod));
    }
}