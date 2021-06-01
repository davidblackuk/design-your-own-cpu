using System;
using System.Drawing;
using Assembler.Exceptions;
using Pastel;

namespace Assembler.Extensions
{
    public static class AssemblerExceptionExtensions
    {
        public static void ToConsole(this AssemblerException exception)
        {
            Console.Write("[ERROR] ".Pastel(Color.Tomato));
            Console.WriteLine(exception.Message.Pastel(Color.Goldenrod));
        }
    }
}