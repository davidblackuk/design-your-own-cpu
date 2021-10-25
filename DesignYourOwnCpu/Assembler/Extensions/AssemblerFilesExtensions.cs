using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Pastel;

namespace Assembler.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AssemblerFilesExtensions
    {
        public static void ToConsole(this IAssemblerFiles files)
        {
            Console.WriteLine();
            OutputFileInfo("Source file:", files.SourceFilename);
            OutputFileInfo("Output file:", files.BinaryFilename);
            OutputFileInfo("Symbol file:", files.SymbolFilename);
            Console.WriteLine();
        }

        private static void OutputFileInfo(string title, string filename)
        {
            Console.Write($"{title} ".Pastel(Color.Goldenrod));
            Console.WriteLine($"{filename} ".Pastel(Color.Orchid));
        }
    }
}