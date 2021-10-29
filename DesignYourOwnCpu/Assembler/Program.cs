using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Assembler.Exceptions;
using Assembler.Extensions;
using Assembler.LineSources;
using Microsoft.Extensions.DependencyInjection;
using Pastel;
using Shared;

namespace Assembler
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(args);
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var files = serviceProvider.GetService<IAssemblerFiles>();

            if (files.SourceFilename == null)
            {
                Usage();
                return;
            }

            files.ToConsole();

            var lineSource =
                new CommentStrippingLineSource(
                    new WhitespaceRemovalLineSource(new FileLineSource(files.SourceFilename)));

            try
            {
                var start = DateTime.Now;

                var assembler = serviceProvider.GetService<IAssembler>();
                assembler.Assemble(lineSource);
                assembler.Ram.Save(files.BinaryFilename);
                Console.WriteLine("\nSymbols\n");
                assembler.SymbolTable.Save(files.SymbolFilename);

                Console.WriteLine($"\nComplete in {(DateTime.Now - start).TotalMilliseconds} (ms)\n");
            }
            catch (AssemblerException e)
            {
                Console.WriteLine($"Emulator error: {e.Message}\n".Pastel(Color.Tomato));
            }
            catch (Exception otherException)
            {
                Console.WriteLine($"Emulator Failure: {otherException.Message}\n".Pastel(Color.Tomato));
            }

            Console.WriteLine();
        }

        private static void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the file to assemble>");
            Console.WriteLine();
            Environment.Exit(0);
        }
    }
}