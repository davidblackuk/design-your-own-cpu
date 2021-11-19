using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Pastel;

namespace Assembler.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AssemblerFilesExtensions
    {
        public static void ToConsole(this IAssemblerConfig config)
        {
            Console.WriteLine();
            OutputFileInfo("Source file:", config.SourceFilename);
            OutputFileInfo("Output file:", config.BinaryFilename);
            OutputFileInfo("Symbol file:", config.SymbolFilename);
            Console.WriteLine();
        }

        private static void OutputFileInfo(string title, string filename)
        {
            Console.Write($"{title} ".Pastel(Color.Goldenrod));
            Console.WriteLine($"{filename} ".Pastel(Color.Orchid));
        }
    }
}