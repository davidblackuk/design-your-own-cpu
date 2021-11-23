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
        private static IServiceProvider ServiceProvider { get; set; }
        private static ILineSource LineSource;
        private static IAssemblerConfig Configuration;

        private static void Main(string[] args)
        {
            ServiceProvider = GetServiceProvider(args);

            Configuration = GetConfiguration();

            LineSource = GetLineSource();

            if (!Configuration.QuietOutput) Configuration.ToConsole();

            try
            {
                var start = DateTime.Now;

                PerformAssembly();

                Console.WriteLine($"\nAssembled {LineSource.ProcessedLines} lines in {(DateTime.Now - start).TotalMilliseconds} (ms)\n");
            }
            catch (AssemblerException e)
            {
                ShowError("Assembler", e);
            }
            catch (Exception otherException)
            {
                ShowError("Internal", otherException);
            }
        }

        /// <summary>
        /// invoke the assembler and save the results
        /// </summary>
        private static void PerformAssembly()
        {
            var assembler = ServiceProvider.GetService<IAssembler>();
            assembler.Assemble(LineSource);
            assembler.Ram.Save(Configuration.BinaryFilename);
            assembler.SymbolTable.Save(Configuration.SymbolFilename);

            if (!Configuration.QuietOutput) assembler.SymbolTable.ToConsole();
        }

        /// <summary>
        /// Initialize or DI container
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IServiceProvider GetServiceProvider(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(args);
            startup.ConfigureServices(services);

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets the configuration settings for the assembler
        /// </summary>
        /// <returns></returns>
        private static IAssemblerConfig GetConfiguration()
        {
            var config = ServiceProvider.GetService<IAssemblerConfig>();
            if (config.SourceFilename == null)
            {
                Usage();
                Environment.Exit(0);
            }
            return config;
        }

        /// <summary>
        /// Create and wire up the chain of responsibility tat processes input lines into a 
        /// stream of non empty, comment free and trimmed lines.
        /// </summary>
        /// <returns></returns>
        private static ILineSource GetLineSource()
        {
            var res = ServiceProvider.GetService<CommentStrippingLineSource>();
            res.ChainTo(ServiceProvider.GetService<WhitespaceRemovalLineSource>())
               .ChainTo(ServiceProvider.GetService<FileLineSource>());
            return res;
        }

        /// <summary>
        /// Report an error to the console
        /// </summary>
        /// <param name="type">Type of error can be assembler, internal etc</param>
        /// <param name="e"></param>
        private static void ShowError(string type, Exception e)
        {
            Console.Write($"{type} error: ".Pastel(Color.Tomato));
            Console.WriteLine(e.Message);
            Console.Write($"\nLine {LineSource.ProcessedLines}: ");
            Console.WriteLine($"{LineSource.CurrentRawLine}\n".Pastel(Color.Teal));
        }
        
        /// <summary>
        /// Show usage information for thwe app and quit execution.
        /// </summary>
        private static void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("    dotnet run  -p <path to project file> --input <path for the file to assemble>");
            Console.WriteLine();
        }
    }
}