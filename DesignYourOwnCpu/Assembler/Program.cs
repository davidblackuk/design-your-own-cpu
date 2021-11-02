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

            var assemblerConfig = serviceProvider.GetService<IAssemblerConfig>();

            if (assemblerConfig.SourceFilename == null)
            {
                Usage();
                return;
            }


            // set up the pipeline of line source from file first, then strip white space, finally strip comments.
            // TODO: Move to startup and wire there
            var lineSource =
                new CommentStrippingLineSource(
                    new WhitespaceRemovalLineSource(
                        new FileLineSource(assemblerConfig.SourceFilename)));

            if (!assemblerConfig.QuietOutput)
            {
                assemblerConfig.ToConsole();
            }
            
            try
            {
                var start = DateTime.Now;

                var assembler = serviceProvider.GetService<IAssembler>();
                assembler.Assemble(lineSource);
                assembler.Ram.Save(assemblerConfig.BinaryFilename);
                assembler.SymbolTable.Save(assemblerConfig.SymbolFilename);
                
                if (!assemblerConfig.QuietOutput)
                {
                    Console.WriteLine("\nSymbols\n");
                    assembler.SymbolTable.ToConsole();
                }
                
                Console.WriteLine($"\nAssembled {lineSource.ProcessedLines} lines in {(DateTime.Now - start).TotalMilliseconds} (ms)\n");
            }
            catch (AssemblerException e)
            {
                Console.WriteLine($"Assembler error: {e.Message}\n".Pastel(Color.Tomato));
            }
            catch (Exception otherException)
            {
                Console.WriteLine($"Assembler Failure: {otherException.Message}\n".Pastel(Color.Tomato));
            }

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